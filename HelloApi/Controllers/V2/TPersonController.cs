using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HelloApi.Data;
using HelloApi.Models.V2; // Add this if TPerson is in Models namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloApi.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class TPersonController : ControllerBase
    {
        private readonly HelloApiContext _context;

        public TPersonController(HelloApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TPerson>>> GetAll()
        {
            return await _context.TPersons.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TPerson>> GetById(int id)
        {
            var person = await _context.TPersons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        [HttpPost]
        public async Task<ActionResult<TPerson>> Create(TPerson person)
        {
            _context.TPersons.Add(person);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TPerson person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TPersonExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _context.TPersons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.TPersons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TPersonExists(int id)
        {
            return _context.TPersons.Any(e => e.Id == id);
        }
    }
}