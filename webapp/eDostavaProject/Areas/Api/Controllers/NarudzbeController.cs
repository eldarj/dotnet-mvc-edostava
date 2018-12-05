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
using eDostava.Web.Areas.Api.Models.RequestModels;
using static eDostava.Web.Areas.Api.Models.NarudzbaListResponse;
using eDostava.Web.Areas.Api.Helper;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Narudzbe")]
    public class NarudzbeController : MyBaseApiController
    {
        public NarudzbeController(MojContext context) : base(context) { }

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
                    .OrderByDescending(n => n.DatumVrijeme)
                    .Select(n => new NarudzbaListResponse.NarudzbaInfo
                    {
                        Id = n.NarudzbaID,
                        GuidSifra = n.Sifra,
                        DatumKreiranja = n.DatumVrijeme,
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

        // POST: api/Narudzbe/Create
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> NewNarudzba([FromBody] CreateNarudzbaRequest Model)
        {
            if (!ModelState.IsValid || Model.stavke.Count == 0)
            {
                return BadRequest("Provjerite narudžbu.");
            }

            try
            {
                Narudzba narudzba = new Narudzba
                {
                    UkupnaCijena = Model.UkupnaCijena,
                    Status = Stanje.NaCekanju,
                    NarucilacID = MyAuthUser.KorisnikID
                };
                _context.Narudzbe.Add(narudzba);
                await _context.SaveChangesAsync();

                foreach (var s in Model.stavke)
                {
                    _context.StavkeNarudzbe.Add(new StavkaNarudzbe
                    {
                        NarudzbaID = narudzba.NarudzbaID,
                        HranaID = s.HranaID,
                        Kolicina = s.Kolicina
                    });
                }

                await _context.SaveChangesAsync();
                return Ok("Nova narudžba kreirana.");
            } catch (Exception e)
            {
                return BadRequest("Couldn't create a new resource, please try again.");
            }

            return BadRequest("Pogrešan username ili password.");
        }

        // DELETE: api/Narudzbe/5/Delete
        [HttpPost("{id}")]
        [Route("{id}/Delete")]
        public async Task<IActionResult> Delete([FromRoute] int Id, [FromBody] UserLoginRequest User)
        {
            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(n => n.Username == User.Username && n.Password == User.Password);
            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            Narudzba narudzba = await _context.Narudzbe.FindAsync(Id);
            if (narudzba == null)
            {
                return NotFound();
            }

            if (narudzba.Status == Stanje.NaCekanju)
            {
                _context.Narudzbe.Remove(narudzba);
                await _context.SaveChangesAsync();

                return Ok("Narudžba uspješno uklonjena.");
            }
            return BadRequest("Dogodila se greška, ili je narudžba već prihvaćena!");
        }

        // POST: api/Narudzbe/5/Status
        [HttpPost("{id}")]
        [Route("{id}/Status")]
        public async Task<IActionResult> Status([FromRoute] int Id, [FromBody] UserLoginRequest User)
        {
            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(n => n.Username == User.Username && n.Password == User.Password);
            if (narucilac == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }

            Narudzba narudzba = await _context.Narudzbe.FindAsync(Id);
            if (narudzba != null)
            {
                return Ok(narudzba.Status.GetDisplay());
            }

            return BadRequest("Dogodila se greška, ili navedena narudžba ne postoji.");
        }
    }
}