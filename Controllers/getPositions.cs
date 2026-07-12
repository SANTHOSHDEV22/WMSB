using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getPositions : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public getPositions(WorkerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Position>>> GetPositions()
        {
            var list = await _context.Positions
                .OrderBy(p => p.Position1)
                .ToListAsync();

            return Ok(list);
        }
    }
}
