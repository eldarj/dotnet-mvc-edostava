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

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Restorani/{id}/Hrana")]
    public class HranaController : Controller
    {
        private readonly MojContext _context;

        public HranaController(MojContext context)
        {
            _context = context;
        }

        // GET: api/Hrana
        [HttpGet]
        public IEnumerable<HranaApiModel.HranaInfo> GetProizvodi()
        {
            int restoranResourceId = Int32.Parse(RouteData.Values["id"].ToString());
            HranaApiModel model = new HranaApiModel
            {
                Hrana = _context.Proizvodi
                .Include(p => p.Jelovnik)
                .Where(p => p.Jelovnik.RestoranID == restoranResourceId)
                .Select(h => new HranaApiModel.HranaInfo
                {
                    Id = h.HranaID,
                    Naziv = h.Naziv,
                    Opis = h.Opis,
                    Slika = HttpContext.Request.Host.Value + "/" + h.Slika,
                    Cijena = h.Cijena,
                    Tip = h.TipKuhinje
                })
                .ToList()
            };
            return model.Hrana;
        }
    }
}