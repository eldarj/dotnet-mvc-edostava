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
using eDostava.Web.Helper;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Narudzbe")]
    public class NarudzbeController : Controller
    {
        private readonly MojContext _context;

        public NarudzbeController(MojContext context)
        {
            _context = context;
        }

        // POST: api/Narudzbe
        [HttpPost]
        public async Task<IActionResult> GetNarudzbe([FromBody] UserLoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Narucilac narucilac = await _context.Narucioci
                .Where(n => n.Username == user.Username && n.Password == user.Password)
                .FirstOrDefaultAsync();

            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            NarudzbaListResponse model = new NarudzbaListResponse
            {
                Narudzbe = _context.Narudzbe
                    .Where(n => n.NarucilacID == narucilac.KorisnikID)
                    .Select(n => new NarudzbaListResponse.NarudzbaInfo
                    {
                        DatumKreiranja = n.DatumVrijeme,
                        GuidSifra = n.Sifra,
                        Status = n.Status.GetDisplay(),
                        UkupnaCijena = n.UkupnaCijena,
                        HranaStavke = n.Stavke.Select(s => new NarudzbaListResponse.NarudzbaHranaStavka
                        {
                            Cijena = s.Hrana.Cijena,
                            Naziv = s.Hrana.Naziv,
                            Kolicina = s.Kolicina
                        })
                            .ToList(),
                        NarucenoIzRestorana = n.Stavke.Select(s => s.Hrana.Jelovnik.Restoran.Naziv).Distinct().ToList()
                    })
                    .ToList()
            };

            return Ok(model);
        }
    }
}