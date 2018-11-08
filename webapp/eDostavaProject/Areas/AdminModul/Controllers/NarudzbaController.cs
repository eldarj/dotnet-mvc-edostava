using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.AdminModul.ViewModels;
using eDostava.Web.Areas.AdminModul.ViewModels.Narudzba;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using eDostava.Web.Areas.AdminModul.Helper;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    public class NarudzbaController : AdminController
    {
        private MojContext context;
        public NarudzbaController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            return View(PrepareAllNarudzbe());
        }

        public NarudzbaPrikazVM PrepareAllNarudzbe()
        {
            return new NarudzbaPrikazVM
            {
                Narudzbe = context.Narudzbe
                    .Select(x => new NarudzbaPrikazVM.NarudzbaPrikazInfo
                    {
                        Id = x.NarudzbaID,
                        Sifra = x.Sifra.ToString(),
                        DatumVrijeme = x.DatumVrijeme,
                        UkupnoStavki = x.Stavke.Count,
                        UkupnaCijena = x.UkupnaCijena,
                        NarucilacImePrezime = x.Narucilac.Ime_prezime,
                        NarucilacId = x.NarucilacID
                    })
                    .OrderByDescending(x => x.DatumVrijeme)
                    .ToList()
            };
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
                Stavke = n.Stavke.ToList(),
                NarucenoIzRestorana = n.Stavke.Select(x => x.Hrana.Jelovnik.Restoran.Naziv).Distinct().ToList()
            });
        }

        public IActionResult Obrisi(int id)
        {
            Narudzba n = context.Narudzbe.Find(id);
            context.Narudzbe.Remove(n);
            context.SaveChanges();

            return PartialView("Index", PrepareAllNarudzbe());
        }
    }
}