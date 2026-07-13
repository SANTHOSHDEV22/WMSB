using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Login : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public Login(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsDeleted == 0);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }


            if (user.PasswordHash == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            bool verified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!verified)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new { user.Id, user.Username, user.Email });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

