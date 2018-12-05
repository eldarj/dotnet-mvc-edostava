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
using eDostava.Web.Areas.Api.Models.RequestModels;
using eDostava.Web.Areas.Api.Helper;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Restorani")]
    public class RestoraniController : MyBaseApiController
    {
        public RestoraniController(MojContext context) : base(context) { }

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
                        .OrderByDescending(r => r.Datum)
                        .Include(r => r.Narucilac)
                        .Select(r => new RestoranListResponse.RestoranRecenzija
                        {
                            Datum = r.Datum,
                            ImePrezime = r.Narucilac.Ime_prezime,
                            Username = r.Narucilac.Username,
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
        

        private List<RestoranListResponse.RestoranRecenzija> getRecenzije(Restoran restoran)
        {
            return _context.Recenzije.Where(r => r.RestoranID == restoran.RestoranID)
                    .Include(r => r.Narucilac)
                    .OrderByDescending(r => r.Datum)
                    .Select(r => new RestoranListResponse.RestoranRecenzija
                    {
                        Datum = r.Datum,
                        ImePrezime = r.Narucilac.Ime_prezime,
                        Username = r.Narucilac.Username,
                        Recenzija = r.Recenzija,
                        ImageUrl = r.Narucilac.ImageUrl
                    }).ToList();
        }

        [HttpPost("{id}")]
        [Route("{id}/Komentari")]
        public async Task<IActionResult> GetKomentare([FromRoute] int id, [FromBody] RestoranKomentariRequest Model)
        {
            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(n => n.Username == Model.credentials.Username && n.Password == Model.credentials.Password);
            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            Restoran restoran = await _context.Restorani.FindAsync(id);
            if (restoran == null)
            {
                return BadRequest("Restoran ne postoji.");
            }

            return Ok(new RestoranKomentariRequest.RestoranKomentariResponse
            {
                Recenzije = await _context.Recenzije.Where(r => r.RestoranID == restoran.RestoranID)
                    .Include(r => r.Narucilac)
                    .OrderByDescending(r => r.Datum)
                    .Select(r => new RestoranListResponse.RestoranRecenzija
                    {
                        Datum = r.Datum,
                        ImePrezime = r.Narucilac.Ime_prezime,
                        Username = r.Narucilac.Username,
                        Recenzija = r.Recenzija,
                        ImageUrl = r.Narucilac.ImageUrl
                    }).ToListAsync()
            });
        }

        [HttpPost("{id}")]
        [Route("{id}/Komentari/Novi")]
        public async Task<IActionResult> NoviKomentar([FromRoute] int id, [FromBody] RestoranNoviKomentarRequest Model)
        {
            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(n => n.Username == Model.credentials.Username && n.Password == Model.credentials.Password);
            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            Restoran restoran = await _context.Restorani.FindAsync(id);
            if (restoran == null)
            {
                return BadRequest("Restoran ne postoji.");
            }

            _context.Recenzije.Add(new RestoranRecenzija
            {
                Datum = DateTime.Now,
                NarucilacID = narucilac.KorisnikID,
                RestoranID = restoran.RestoranID,
                Recenzija = Model.komentar
            });
            await _context.SaveChangesAsync();


            return Ok(new RestoranKomentariRequest.RestoranKomentariResponse
            {
                Recenzije = await _context.Recenzije.Where(r => r.RestoranID == restoran.RestoranID)
                    .Include(r => r.Narucilac)
                    .OrderByDescending(r => r.Datum)
                    .Select(r => new RestoranListResponse.RestoranRecenzija
                    {
                        Datum = r.Datum,
                        ImePrezime = r.Narucilac.Ime_prezime,
                        Username = r.Narucilac.Username,
                        Recenzija = r.Recenzija,
                        ImageUrl = r.Narucilac.ImageUrl
                    }).ToListAsync()
            });
        }

        [HttpPost("{id}")]
        [Route("{id}/Komentari/Subscribe")]
        public async Task<IActionResult> SubscribeKomentari([FromRoute] int id, [FromBody] RestoranKomentariRequest subRequest)
        {
            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(n => n.Username == subRequest.credentials.Username && n.Password == subRequest.credentials.Password);
            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            Restoran restoran = await _context.Restorani.FindAsync(id);
            if (restoran == null)
            {
                return BadRequest("Restoran ne postoji.");
            }

            var recenzijeList = getRecenzije(restoran);
            if (subRequest.KomentariHashCode == null || subRequest.KomentariHashCode.Length == 0)
            {
                return Ok(new RestoranKomentariRequest.RestoranKomentariResponse
                {
                    KomentariHashCode = generateUniqueHash(recenzijeList)
                });
            } else
            {
                try
                {
                    // keep the connection alive and keep checking data source hash vs client's data hash
                    do
                    {
                        //
                    }
                    while (subRequest.KomentariHashCode == generateUniqueHash(getRecenzije(restoran)));

                    // data source changed if hash changed
                    return Ok(new RestoranKomentariRequest.RestoranKomentariResponse
                    {
                        KomentariHashCode = generateUniqueHash(getRecenzije(restoran)),
                        Recenzije = getRecenzije(restoran)
                    });
                } catch (Exception e)
                {
                    return BadRequest("Connection closed - " + e.Message);
                }
            }

            return BadRequest("Connection closed.");
        }

        private string generateUniqueHash(List<RestoranListResponse.RestoranRecenzija> recenzije)
        {
            string hash = "";
            foreach (RestoranListResponse.RestoranRecenzija r in recenzije)
            {
                hash += r.Recenzija.Replace(" ", "") + r.Username;
            }
            return hash;
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