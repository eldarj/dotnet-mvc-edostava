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

        // GET: api/Narudzbe
        [HttpGet]
        public IEnumerable<NarudzbaApiModel.NarudzbaInfo> GetNarudzbe()
        {
            NarudzbaApiModel model = new NarudzbaApiModel
            {
                Narudzbe = _context.Narudzbe
                    .Select(n => new NarudzbaApiModel.NarudzbaInfo
                    {
                        DatumKreiranja = n.DatumVrijeme,
                        GuidSifra = n.Sifra,
                        Status = n.Status.GetDisplay(),
                        UkupnaCijena = n.UkupnaCijena,
                        HranaStavke = n.Stavke.Select(s => new NarudzbaApiModel.NarudzbaHranaStavka
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

            return model.Narudzbe;
        }

        // GET: api/Narudzbe/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNarudzba([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var narudzba = await _context.Narudzbe.SingleOrDefaultAsync(m => m.NarudzbaID == id);

            if (narudzba == null)
            {
                return NotFound();
            }

            return Ok(new NarudzbaApiModel.NarudzbaInfo
            {
                DatumKreiranja = narudzba.DatumVrijeme,
                GuidSifra = narudzba.Sifra,
                Status = narudzba.Status.GetDisplay(),
                UkupnaCijena = narudzba.UkupnaCijena,
                HranaStavke = narudzba.Stavke.Select(s => new NarudzbaApiModel.NarudzbaHranaStavka
                {
                    Cijena = s.CalcCijena,
                    Naziv = s.Hrana.Naziv,
                    Kolicina = s.Kolicina
                })
                            .ToList(),
                NarucenoIzRestorana = narudzba.Stavke.Select(s => s.Hrana.Jelovnik.Restoran.Naziv).ToList()
            });
        }

        // POST: api/Narudzbe
        [HttpPost]
        public async Task<IActionResult> PostNarudzba([FromBody] Narudzba narudzba)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Narudzbe.Add(narudzba);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNarudzba", new { id = narudzba.NarudzbaID }, narudzba);
        }
    }
}