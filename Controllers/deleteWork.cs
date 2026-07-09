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
            if (worker == null)
            {
                return NotFound();
            }
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}