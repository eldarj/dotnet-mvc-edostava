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
using static eDostava.Web.Areas.AdminModul.ViewModels.RestoranPrikazVM;
using eDostava.Web.Areas.Api.Helper;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Restorani/{id}/Hrana")]
    public class HranaController : MyBaseApiController
    { 
        public HranaController(MojContext context) : base(context) { }

        // GET: api/Restorani/{id}/Hrana
        [HttpGet]
        public ActionResult GetProizvodi([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int restoranId = Int32.Parse(RouteData.Values["id"].ToString());
            HranaListResponse model = new HranaListResponse
            {
                Hrana = _context.Proizvodi
                .Include(p => p.Jelovnik)
                .Where(p => p.Jelovnik.RestoranID == restoranId)
                .Select(h => new HranaListResponse.HranaInfo
                {
                    Id = h.HranaID,
                    Naziv = h.Naziv,
                    Opis = h.Opis,
                    ImageUrl = HttpContext.Request.Host.Value + "/" +  h.Slika,
                    Cijena = h.Cijena,
                    TipKuhinje = h.TipKuhinje,
                    RestoranNaziv = h.Jelovnik.Restoran.Naziv,
                    RestoranId = h.Jelovnik.RestoranID
                })
                .ToList()
            };

            return Ok(model);
        }
    }
}