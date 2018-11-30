using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eDostava.Data;
using eDostava.Data.Models;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Narucilacs")]
    public class NarucilacsController : Controller
    {
        private readonly MojContext _context;

        public NarucilacsController(MojContext context)
        {
            _context = context;
        }

        // GET: api/Narucilacs
        [HttpGet]
        public IEnumerable<Narucilac> GetNarucioci()
        {
            return _context.Narucioci;
        }

        // GET: api/Narucilacs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNarucilac([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var narucilac = await _context.Narucioci.SingleOrDefaultAsync(m => m.KorisnikID == id);

            if (narucilac == null)
            {
                return NotFound();
            }

            return Ok(narucilac);
        }

        // PUT: api/Narucilacs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNarucilac([FromRoute] int id, [FromBody] Narucilac narucilac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != narucilac.KorisnikID)
            {
                return BadRequest();
            }

            _context.Entry(narucilac).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NarucilacExists(id))
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

        // POST: api/Narucilacs
        [HttpPost]
        public async Task<IActionResult> PostNarucilac([FromBody] Narucilac narucilac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Narucioci.Add(narucilac);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNarucilac", new { id = narucilac.KorisnikID }, narucilac);
        }

        // DELETE: api/Narucilacs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNarucilac([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var narucilac = await _context.Narucioci.SingleOrDefaultAsync(m => m.KorisnikID == id);
            if (narucilac == null)
            {
                return NotFound();
            }

            _context.Narucioci.Remove(narucilac);
            await _context.SaveChangesAsync();

            return Ok(narucilac);
        }

        private bool NarucilacExists(int id)
        {
            return _context.Narucioci.Any(e => e.KorisnikID == id);
        }
    }
}