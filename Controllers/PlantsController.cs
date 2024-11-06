using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExoticPlantsAPI.Models;

namespace ExoticPlantsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantsController : ControllerBase
    {
        private readonly ExoticPlantsContext _context;

        public PlantsController(ExoticPlantsContext context)
        {
            _context = context;
        }

        // GET: api/Plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExoticPlant>>> GetExoticPlants()
        {
            return await _context.ExoticPlants.ToListAsync();
        }

        // GET: api/Plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExoticPlant>> GetExoticPlant(int id)
        {
            var exoticPlant = await _context.ExoticPlants.FindAsync(id);

            if (exoticPlant == null)
            {
                return NotFound();
            }

            return exoticPlant;
        }

        // PUT: api/Plants/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExoticPlant(int id, ExoticPlant exoticPlant)
        {
            if (id != exoticPlant.ID)
            {
                return BadRequest();
            }

            _context.Entry(exoticPlant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExoticPlantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Plants
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExoticPlant>> PostExoticPlant(ExoticPlant exoticPlant)
        {
            _context.ExoticPlants.Add(exoticPlant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExoticPlant", new { id = exoticPlant.ID }, exoticPlant);
        }

        // DELETE: api/Plants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExoticPlant(int id)
        {
            var exoticPlant = await _context.ExoticPlants.FindAsync(id);
            if (exoticPlant == null)
            {
                return NotFound();
            }

            _context.ExoticPlants.Remove(exoticPlant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExoticPlantExists(int id)
        {
            return _context.ExoticPlants.Any(e => e.ID == id);
        }
    }
}
