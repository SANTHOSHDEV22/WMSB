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
                Console.WriteLine("[LOGIN DEBUG] ModelState invalid.");
                return BadRequest(ModelState);
            }

            Console.WriteLine($"[LOGIN DEBUG] Attempt for email: '{request.Email}'");

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email && u.IsDeleted == 0);

            if (user == null)
            {
                Console.WriteLine("[LOGIN DEBUG] No matching user found (email mismatch or IsDeleted=1).");
                return Unauthorized("Invalid email or password.");
            }

            Console.WriteLine($"[LOGIN DEBUG] User found: Id={user.Id}, PasswordHash present={user.PasswordHash != null}, GoogleId={user.GoogleId}");

            if (user.PasswordHash == null)
            {
                Console.WriteLine("[LOGIN DEBUG] PasswordHash is NULL - this account was likely created via Google sign-up.");
                return Unauthorized("Invalid email or password.");
            }

            bool verified = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            Console.WriteLine($"[LOGIN DEBUG] BCrypt.Verify result: {verified}");

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

