using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class updatePosition : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public updatePosition(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdatePosition(int id, [FromBody] UpdatePositionRequest request)
        {
            var worker = await _context.Workers.FindAsync(id);

            if (worker == null || worker.IsDeleted == 1)
                return NotFound("Worker not found.");

            if (request.PositionId.HasValue)
            {
                bool positionExists = await _context.Positions.AnyAsync(p => p.Id == request.PositionId.Value);
                if (!positionExists)
                    return BadRequest("Invalid PositionId - no matching position exists.");
            }

            worker.PositionId = request.PositionId;
            worker.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { worker.Id, worker.PositionId });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database error: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }

    public class UpdatePositionRequest
    {
        public int? PositionId { get; set; }
    }
}