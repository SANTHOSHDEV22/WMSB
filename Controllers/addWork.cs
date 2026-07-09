using Microsoft.AspNetCore.Mvc;
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
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetWorker", new { id = worker.Id }, worker);
        }

    }
}
