using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.Areas.KorisnikModul.ViewModels;
using RS1_Ispit_2017.Helper;
using eDostava.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace eDostava.Web.Areas.KorisnikModul.Controllers
{
    [Area("KorisnikModul")]
    public class NarudzbaController : Controller
    {
        private MojContext context;
        public NarudzbaController(MojContext db)
        {
            context = db;
        }
        public IActionResult Index()
        {
            int trenutniNarucilacID = HttpContext.GetLogiranogNarucioca().KorisnikID;
            return PartialView(new NarudzbaPrikazVM
            {
                Narudzbe = context.Narudzbe
                    .Where(x => x.NarucilacID == trenutniNarucilacID)
                    .Select(x => new NarudzbaPrikazVM.NarudzbaPrikazInfo
                    {
                        NarudzbaId = x.NarudzbaID,
                        Sifra = x.Sifra.ToString(),
                        DatumVrijeme = x.DatumVrijeme,
                        UkupnaCijena = x.UkupnaCijena
                    })
                    .OrderByDescending(x => x.DatumVrijeme)
                    .ToList()
            });
        }

        public IActionResult PosljednjaNarudzba()
        {
            Narudzba PosljednjaNarudzba = context.Narudzbe
                .Where(x => x.NarucilacID == HttpContext.GetLogiranogNarucioca().KorisnikID)
                .First();

            return RedirectToAction("Detaljno", new { id = PosljednjaNarudzba.NarudzbaID });
        }

        public IActionResult Detaljno(int id)
        {
            Narudzba n = context.Narudzbe
                .Include(x => x.Stavke)
                .ThenInclude(x => x.Hrana)
                .ThenInclude(x => x.Jelovnik)
                .ThenInclude(x => x.Restoran)
                .Where(x => x.NarudzbaID == id)
                .FirstOrDefault();

            return PartialView(new NarudzbaDetaljnoVM
            {
                Sifra = n.Sifra.ToString(),
                DatumVrijeme = n.DatumVrijeme,
                UkupnaCijena = n.UkupnaCijena,
                Stavke = n.Stavke.ToList(),
                NarucenoIzRestorana = n.Stavke.Select(x => x.Hrana.Jelovnik.Restoran.Naziv).Distinct().ToList()
            });
        }
    }
}