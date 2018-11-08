using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.AdminModul.ViewModels;
using eDostava.Web.Areas.AdminModul.Helper;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    public class GradController : AdminController
    {
        private MojContext context;
        public GradController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            return View(PrepareAllGradove());
        }

        public IActionResult IndexPartial()
        {
            return PartialView("Index", PrepareAllGradove());
        }

        public GradPrikazVM PrepareAllGradove()
        {
            return new GradPrikazVM
            {
                Gradovi = context.Gradovi
                   .Select(x => new GradPrikazVM.GradInfo
                   {
                       Id = x.GradID,
                       Naziv = x.Naziv,
                       PostanskiBroj = x.PoštanskiBroj,
                       UkupnoNarucioca = context.Narucioci.Where(s => s.Blok.GradID == x.GradID).Count(),
                       Narucioci = context.Narucioci.Where(s => s.Blok.GradID == x.GradID).ToList(),
                       UkupnoBlokova = context.Blokovi.Where(s => s.GradID == x.GradID).Count(),
                       Blokovi = context.Blokovi.Where(s => s.GradID == x.GradID).ToList()
                   })
                   .ToList()
            };
        }

        public IActionResult Dodaj()
        {
            return PartialView("Uredi", new GradUrediVM());
        }

        public IActionResult Uredi(int id)
        {
            Grad x = context.Gradovi.Find(id);

            return PartialView(new GradUrediVM
            {
                Id = x.GradID,
                Naziv = x.Naziv,
                PostanskiBroj = x.PoštanskiBroj
            });
        }

        public IActionResult Snimi(GradUrediVM Model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("Uredi", Model);
            }

            Grad grad;
            if (Model.Id == 0)
            {
                grad = new Grad();
                context.Gradovi.Add(grad);
            }
            else
            {
                grad = context.Gradovi.Find(Model.Id);
            }

            grad.Naziv = Model.Naziv;
            grad.PoštanskiBroj = Model.PostanskiBroj;

            context.SaveChanges();

            return RedirectToAction("IndexPartial");
        }

        public IActionResult Obrisi(int id)
        {
            Grad x = context.Gradovi.Find(id);

            context.Gradovi.Remove(x);
            context.SaveChanges();

            return RedirectToAction("IndexPartial");
        }
    }
}