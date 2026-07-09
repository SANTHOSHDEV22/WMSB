using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMSB.Models;

namespace WMSB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class addWorker : ControllerBase
    {
        private readonly WorkerDbContext _context;

        public addWorker(WorkerDbContext context)
        {
            _context = context;
        }

        //GET: api/addWorker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> GetWorkers()
        {
            return await _context.Workers.ToListAsync();
        }

        //// GET: api/addWorker/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Worker>> GetWorker(int id)
        //{
        //    var worker = await _context.Workers.FindAsync(id);

        //    if (worker == null)
        //    {
        //        return NotFound();
        //    }

        //    return worker;
        //}

        // PUT: api/addWorker/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWorker(int id, Worker worker)
        //{
        //    if (id != worker.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(worker).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WorkerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/addWorker
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Worker>> PostWorker(Worker worker)
        //{
        //    _context.Workers.Add(worker);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetWorker", new { id = worker.Id }, worker);
        //}

        // DELETE: api/addWorker/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteWorker(int id)
        //{
        //    var worker = await _context.Workers.FindAsync(id);
        //    if (worker == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Workers.Remove(worker);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool WorkerExists(int id)
        //{
        //    return _context.Workers.Any(e => e.Id == id);
        //}
    }
}
