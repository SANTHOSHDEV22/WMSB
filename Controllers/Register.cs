using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public Register(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.IsDeleted == 0))
                return Conflict("This email is already registered.");

            //if (await _context.Users.AnyAsync(u => u.Username == request.Username && u.IsDeleted == 0))
            //    return Conflict("This username is already taken.");

            //if (!string.IsNullOrEmpty(request.Contact) &&
            //    await _context.Users.AnyAsync(u => u.Contact == request.Contact && u.IsDeleted == 0))
            //    return Conflict("This contact number is already registered.");

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Contact = request.Contact,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = 0
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(new { user.Id, user.Username, user.Email });

        }
    }

    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Contact { get; set; }
        public string Password { get; set; } = null!;
    }
}
