using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.AdminModul.ViewModels;
using eDostava.Web.Areas.Api.Models;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Restorani")]
    public class RestoraniController : Controller
    {
        private readonly MojContext _context;

        public RestoraniController(MojContext context)
        {
            _context = context;
        }

        // GET: api/Restorani
        [HttpGet]
        public ActionResult GetRestorani()
        {
            RestoranApiModel model = new RestoranApiModel
            {
                Restorani = _context.Restorani
                .Select(x => new RestoranApiModel.RestoranInfo
                {
                    Id = x.RestoranID,
                    Naziv = x.Naziv,
                    Opis = x.Opis,
                    Vlasnik = x.Vlasnik,
                    Telefon = x.Telefon,
                    Lokacija = x.Blok.Grad.Naziv + ", " + x.Blok.Naziv,
                    Lajkovi = _context.Lajkovi.Where(l => l.RestoranID == x.RestoranID).Select( l => new RestoranApiModel.RestoranLike
                        {
                            Datum = l.Datum,
                            ImePrezime = l.Narucilac.Ime_prezime,
                            Recenzija = l.Recenzija
                        }).ToList(),
                    Slika = HttpContext.Request.Host.Value + "/" + x.Slika,
                    Slogan = x.Slogan,
                    WebUrl = x.WebUrl,
                    TipoviKuhinje = _context.Proizvodi
                                        .Where(p => p.Jelovnik.RestoranID == x.RestoranID).Select(p => p.TipKuhinje).Distinct().ToList()
            })
                .ToList()
            };

            return Ok(model);
        }

        // GET: api/Restorani/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestoran([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var restoran = await _context.Restorani
                .Include(m => m.Vlasnik)
                .Include(m => m.Blok)
                .ThenInclude(m => m.Grad)
                .SingleOrDefaultAsync(m => m.RestoranID == id);

            if (restoran == null)
            {
                return NotFound();
            }

            return Ok(new RestoranApiModel.RestoranInfo
            {
                Id = restoran.RestoranID,
                Naziv = restoran.Naziv,
                Opis = restoran.Opis,
                Vlasnik = restoran.Vlasnik,
                Telefon = restoran.Telefon,
                Lokacija = restoran.Blok.Grad.Naziv + ", " + restoran.Blok.Naziv,
                Lajkovi = _context.Lajkovi.Where(l => l.RestoranID == restoran.RestoranID).Select(l => new RestoranApiModel.RestoranLike
                {
                    Datum = l.Datum,
                    ImePrezime = l.Narucilac.Ime_prezime,
                    Recenzija = l.Recenzija
                }).ToList(),
                Slika = HttpContext.Request.Host.Value + "/" + restoran.Slika,
                Slogan = restoran.Slogan,
                WebUrl = restoran.WebUrl,
                TipoviKuhinje = _context.Proizvodi
                                        .Where(p => p.Jelovnik.RestoranID == restoran.RestoranID).Select(p => p.TipKuhinje).Distinct().ToList()
            });
        }
    }
}