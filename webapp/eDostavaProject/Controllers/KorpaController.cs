using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.ViewModels;
using KorpaSessionExtensions;
using eDostava.Data.Models;
using eDostava.Web.Helper;

namespace eDostava.Web.Controllers
{
    public class KorpaController : Controller
    {
        private MojContext context;

        public KorpaController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index() => ViewComponent("Korpa");

        public IActionResult Dodaj(int id)
        {
            Narudzba narudzba = HttpContext.GetNarudzba();

            List<StavkaNarudzbe> stavke = HttpContext.GetStavke();

            Hrana _proizvod = context.Proizvodi.Where(x => x.HranaID == id).FirstOrDefault();

            StavkaNarudzbe _stavka = new StavkaNarudzbe()
            {
                Narudzba = narudzba,
                NarudzbaID = narudzba.NarudzbaID,
                Hrana = _proizvod,
                HranaID = _proizvod.HranaID,
                Kolicina = 1
            };

            if (stavke.AddUnique(_stavka))
            {
                narudzba.Status = Stanje.NaCekanju;
                narudzba.UkupnaCijena += _stavka.CalcCijena;
                HttpContext.SetNarudzba(narudzba);
                HttpContext.SetStavke(stavke);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Ukloni(int id)
        {
            Narudzba narudzba = HttpContext.GetNarudzba();
            List<StavkaNarudzbe> stavke = HttpContext.GetStavke();
            StavkaNarudzbe stavka = stavke.Find(x => x.HranaID == id);

            if (stavke.UmanjiKolicinu(stavka))
            {
                narudzba.UkupnaCijena -= stavka.Hrana.Cijena;

                if (!stavke.Any())
                {
                    narudzba.Status = Stanje.Prazna;
                }

                HttpContext.SetNarudzba(narudzba);
                HttpContext.SetStavke(stavke);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Naruci()
        {
            Narudzba narudzba = HttpContext.GetNarudzba();
            List<StavkaNarudzbe> stavke = HttpContext.GetStavke();

            context.Narudzbe.Add(narudzba);
            context.SaveChanges();

            foreach (var s in stavke)
            {
                context.StavkeNarudzbe.Add(new StavkaNarudzbe {
                    NarudzbaID = narudzba.NarudzbaID,
                    HranaID = s.HranaID,
                    Kolicina = s.Kolicina
                });
            }
            context.SaveChanges();

            HttpContext.IsporuciNarudzbu();
            HttpContext.InitStavke();

            return RedirectToAction("Index");
        }

        public IActionResult Odbaci()
        {
            HttpContext.InitStavke();
            HttpContext.InitNarudzba();
            return RedirectToAction("Index");
        }
    }
}