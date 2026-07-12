using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class punchDetail : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public punchDetail(WorkerDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult> GetStatus()
        //{
        //    var last = await _context.PunchRecords
        //        .OrderByDescending(p => p.Id)
        //        .FirstOrDefaultAsync();

        //    bool isSignedIn = last != null && last.PunchType == "IN";

        //    DateTime? signInTimestamp = isSignedIn
        //        ? last!.PunchDate.ToDateTime(TimeOnly.FromTimeSpan(last.PunchTime))
        //        : null;

        //    return Ok(new
        //    {
        //        isSignedIn,
        //        signInTimestamp
        //    });
        //}

        [HttpPost]
        public async Task<ActionResult> Punch([FromBody] PunchRequest request)
        {
            if (request.PunchType != "IN" && request.PunchType != "OUT")
                return BadRequest("PunchType must be 'IN' or 'OUT'.");

            var last = await _context.PunchRecords
                .OrderByDescending(p => p.Id)
                .FirstOrDefaultAsync();

            bool isSignedIn = last != null && last.PunchType == "IN";

            if (request.PunchType == "IN" && isSignedIn)
                return BadRequest("Already signed in.");
            if (request.PunchType == "OUT" && !isSignedIn)
                return BadRequest("Not currently signed in.");

            var now = DateTime.Now;
            var record = new PunchRecord
            {
                PunchType = request.PunchType,
                PunchDate = DateOnly.FromDateTime(now),
                PunchTime = now.TimeOfDay,
                CreatedAt = DateTime.UtcNow
            };

            _context.PunchRecords.Add(record);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                record.Id,
                record.PunchType,
                timestamp = now
            });

        }
    }

    public class PunchRequest
    {
        public string PunchType { get; set; } = null!;
    }
}
