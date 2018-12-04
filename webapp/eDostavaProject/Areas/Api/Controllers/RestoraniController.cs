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
            RestoranListResponse model = new RestoranListResponse
            {
                Restorani = _context.Restorani
                .Select(x => new RestoranListResponse.RestoranInfo
                {
                    Id = x.RestoranID,
                    Naziv = x.Naziv,
                    Opis = x.Opis,
                    Vlasnik = x.Vlasnik,
                    Telefon = x.Telefon,
                    Adresa = x.Adresa,
                    Lokacija = x.Blok.Grad.Naziv + ", " + x.Blok.Naziv,
                    Recenzije = _context.Recenzije.Where(r => r.RestoranID == x.RestoranID)
                        .Include(r => r.Narucilac)
                        .Select(r => new RestoranListResponse.RestoranRecenzija
                        {
                            Datum = r.Datum,
                            ImePrezime = r.Narucilac.Ime_prezime,
                            Username = r.Narucilac.Username,
                            Liked = _context.Lajkovi.Where(l => l.NarucilacID == r.NarucilacID && l.RestoranID == r.RestoranID).SingleOrDefault() == null ? false : true,
                            Recenzija = r.Recenzija,
                            ImageUrl = r.Narucilac.ImageUrl
                        }).ToList(),
		            Lajkovi = _context.Lajkovi.Where(l => l.RestoranID == x.RestoranID)
			        .Include(l => l.Narucilac)
			        .Select(l => new RestoranListResponse.RestoranLike
			        {
			           Username = l.Narucilac.Username,
			           ImePrezime = l.Narucilac.Ime_prezime,
			           ImageUrl = l.Narucilac.ImageUrl,
			        }).ToList(),
                    LikeCount = _context.Lajkovi.Where(l => l.RestoranID == x.RestoranID).Count(),
                    Slika = HttpContext.Request.Host.Value + "/" + x.Slika,
                    Slogan = x.Slogan,
                    WebUrl = x.WebUrl,
                    Email = x.Email,
                    TipoviKuhinje = _context.Proizvodi
                                        .Where(p => p.Jelovnik.RestoranID == x.RestoranID).Select(p => p.TipKuhinje).Distinct().ToList()
            })
                .ToList()
            };

            return Ok(model);
        }

        // GET: api/Restorani/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleRestoran([FromRoute] int id)
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

            return Ok(new RestoranListResponse.RestoranInfo
            {
                Id = restoran.RestoranID,
                Naziv = restoran.Naziv,
                Opis = restoran.Opis,
                Vlasnik = restoran.Vlasnik,
                Telefon = restoran.Telefon,
                Adresa = restoran.Adresa,
                Lokacija = restoran.Blok.Grad.Naziv + ", " + restoran.Blok.Naziv,
                Recenzije = _context.Recenzije.Where(r => r.RestoranID == restoran.RestoranID)
                    .Include(r => r.Narucilac)
                    .Select(r => new RestoranListResponse.RestoranRecenzija
                    {
                        Datum = r.Datum,
                        ImePrezime = r.Narucilac.Ime_prezime,
                        Username = r.Narucilac.Username,
                        Liked = _context.Lajkovi.First(l => l.NarucilacID == r.NarucilacID && l.RestoranID == r.RestoranID) == null ? false : true,
                        Recenzija = r.Recenzija,
                        ImageUrl = r.Narucilac.ImageUrl
                    }).ToList(),
                LikeCount = _context.Lajkovi.Where(l => l.RestoranID == restoran.RestoranID).Count(),
                Slika = HttpContext.Request.Host.Value + "/" + restoran.Slika,
                Slogan = restoran.Slogan,
                Email = restoran.Email,
                WebUrl = restoran.WebUrl,
                TipoviKuhinje = _context.Proizvodi
                                        .Where(p => p.Jelovnik.RestoranID == restoran.RestoranID).Select(p => p.TipKuhinje).Distinct().ToList()
            });
        }



        // POST: api/Restorani/5/Like
        [HttpPost("{id}")]
        [Route("{id}/Like")]
        public async Task<IActionResult> LikeRestoran([FromRoute] int id, [FromBody] UserLoginRequest User)
        {
            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(n => n.Username == User.Username && n.Password == User.Password);
            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            Restoran restoran = await _context.Restorani.FindAsync(id);

            if (restoran != null && _context.Lajkovi.SingleOrDefault(l => l.NarucilacID == narucilac.KorisnikID && l.RestoranID == restoran.RestoranID) == null)
            {
                RestoranLike like = new RestoranLike
                {
                    RestoranID = restoran.RestoranID,
                    NarucilacID = narucilac.KorisnikID,
                    Datum = DateTime.Now
                };

                _context.Lajkovi.Add(like);
                await _context.SaveChangesAsync();

                return Ok("Dodali ste restoran \"" + restoran.Naziv + " \" u omiljene!.");
            }

            return BadRequest("Nismo pronašli restoran, ili je user već lajkao isti restoran.");
        }

        // POST: api/Restorani/5/Unlike
        [HttpPost("{id}")]
        [Route("{id}/Unlike")]
        public async Task<IActionResult> UnlikeRestoran([FromRoute] int id, [FromBody] UserLoginRequest User)
        {
            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(n => n.Username == User.Username && n.Password == User.Password);
            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            Restoran restoran = await _context.Restorani.FindAsync(id);
            RestoranLike like = await _context.Lajkovi.FirstAsync(l => l.NarucilacID == narucilac.KorisnikID && l.RestoranID == restoran.RestoranID);

            if (restoran != null && like != null)
            {
                _context.Lajkovi.Remove(like);
                await _context.SaveChangesAsync();

                return Ok("Uklonili ste restoran \"" + restoran.Naziv + "\" iz omiljenih.");
            }

            return BadRequest("Nismo pronašli restoran, ili restoran nije ni bio lajkan.");
        }
    }
}