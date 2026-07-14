using Microsoft.AspNetCore.Mvc;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class deleteWork : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public deleteWork(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            var worker = await _context.Workers.FindAsync(id);
            if (worker == null || worker.IsDeleted == 1)
            {
                return NotFound("Worker not found.");
            }

            worker.IsDeleted = 1;
            worker.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}