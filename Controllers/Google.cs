using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class Google : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public Google(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> GoogleAuth([FromBody] GoogleAuthRequest request)
        {
            GoogleJsonWebSignature.Payload payload;

            try
            {
                payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);
            }
            catch (InvalidJwtException)
            {
                return Unauthorized("Invalid Google token.");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u =>
                (u.GoogleId == payload.Subject || u.Email == payload.Email) && u.IsDeleted == 0);

            if (user == null)
            {
                user = new User
                {
                    Username = await GenerateUniqueUsername(payload.Email),
                    Email = payload.Email,
                    Contact = null,
                    PasswordHash = null,
                    GoogleId = payload.Subject,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsDeleted = 0
                };
                _context.Users.Add(user);
            }
            else if (string.IsNullOrEmpty(user.GoogleId))
            {
                user.GoogleId = payload.Subject;
                user.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return Ok(new { user.Id, user.Username, user.Email });
        }

        private async Task<string> GenerateUniqueUsername(string email)
        {
            var baseName = email.Split('@')[0];
            var candidate = baseName;
            var suffix = 1;

            while (await _context.Users.AnyAsync(u => u.Username == candidate))
            {
                candidate = $"{baseName}{suffix}";
                suffix++;
            }

            return candidate;
        }
    }

    public class GoogleAuthRequest
    {
        public string IdToken { get; set; } = null!;
    }
}
