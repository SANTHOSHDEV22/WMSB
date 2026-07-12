using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class addWork : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public addWork(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Worker>> AddWork([FromBody] Worker worker)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool emailExists = await _context.Workers
                .AnyAsync(w => w.Email == worker.Email && w.IsDeleted == 0);

            if (emailExists)
                return Conflict("A worker with this email already exists.");

            worker.CreatedAt = DateTime.UtcNow;
            worker.UpdatedAt = DateTime.UtcNow;
            worker.IsDeleted = 0;

            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();

            worker.AssociateId = worker.Id;
            await _context.SaveChangesAsync();

            return Ok(worker);

        }
    }
}
