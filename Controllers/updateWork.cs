using Microsoft.AspNetCore.Mvc;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class updateWork : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public updateWork(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWork(int id, Worker worker)
        {
            //DateTime utcTime = DateTime.UtcNow;
            //string strUtcTime_s = utcTime.ToString("s");



            var existing = await _context.Workers.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Id = existing.Id;
            existing.AssociateId = existing.AssociateId;
            existing.FirstName = worker.FirstName;
            existing.LastName = worker.LastName;
            existing.Email = worker.Email;
            existing.Phone = worker.Phone;
            existing.PositionId = existing.PositionId;
            existing.CreatedAt = existing.CreatedAt;
            existing.UpdatedAt = existing.UpdatedAt;
            existing.IsDeleted = existing.IsDeleted;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

    }
}
