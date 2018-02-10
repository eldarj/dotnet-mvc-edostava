using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.ViewModels;
using eDostava.Data.Models;

namespace eDostava.Web.Controllers
{
    public class GradController : Controller
    {
        private MojContext context;

        public GradController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            var model = new GradPrikazVM
            {
                Rows = context.Gradovi
               .Select(x => new GradPrikazVM.Row
               {
                   GradId = x.GradID,
                   Naziv = x.Naziv,
                   PostanskiBroj = x.PoštanskiBroj,
                   UkupnoNarucioca = context.Narucioci.Where(s => s.Blok.GradID == x.GradID).Count(),
               })
               .ToList()
            };
            return View(model);
        }

        public IActionResult Obrisi(int id)
        {
            Grad x = context.Gradovi.Find(id);
            context.Gradovi.Remove(x);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Dodaj()
        {
            return View("Uredi", new GradUrediVM());
        }

        public IActionResult Uredi(int id)
        {
            Grad x = context.Gradovi.Find(id);
            return View(new GradUrediVM
            {
                GradId = x.GradID,
                Naziv = x.Naziv,
                PostanskiBroj = x.PoštanskiBroj,
                BrojBlokova = context.Blokovi.Where(w => w.GradID == x.GradID).Count()
            });
        }

        public IActionResult Snimi(int id, string naziv, int postanskiBroj)
        {
            context.Gradovi.Add(new Grad
            {
                Naziv = naziv,
                PoštanskiBroj = postanskiBroj
            });

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}