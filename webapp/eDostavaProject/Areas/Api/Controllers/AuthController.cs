using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.Api.Models;
using System.Net;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly MojContext _context;

        public AuthController(MojContext context)
        {
            _context = context;
        }

        // POST: api/Auth
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthLoginPost postAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthUserVM model = await _context.Narucioci
                .Where(n => n.Username == postAccount.Username && n.Password == postAccount.Password)
                .Select(s => new AuthUserVM
                {
                    Id = s.KorisnikID,
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Username = s.Username,
                    Blok = s.Blok,
                    Token ="",
                    Adresa = s.Adresa,
                    ImageUrl = s.ImageUrl
                })
                .FirstOrDefaultAsync();


            if (model != null)
            {
                // due to lazyloading - fix this later
                model.Blok.Grad = _context.Gradovi.Where(g => g.GradID == model.Blok.GradID).FirstOrDefault();
                return Ok(model);
            }

            return BadRequest("Pogrešan username ili password.");
        }


        // POST: api/Auth/Register
        [HttpPost]
        [Route("api/Auth/Register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterPost Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Narucilac newUser = new Narucilac
            {
                Ime = Model.Ime,
                Prezime = Model.Prezime,
                Username = Model.Username,
                BadgeID = 1,
                DatumKreiranja = DateTime.Now,
                BlokID = Model.BlokID
            };

            _context.Narucioci.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNarucilac", new { id = newUser.KorisnikID }, newUser);
        }

        // DELETE: api/Auth/5
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