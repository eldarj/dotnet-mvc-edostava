using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.AdminModul.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    [Area("AdminModul")]
    public class BlokController : Controller
    {
        private MojContext context;
        public BlokController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            return View(GetAllBlokoviVM());
        }

        private BlokPrikazVM GetAllBlokoviVM()
        { 
            return new BlokPrikazVM
            {
                Blokovi = context.Blokovi
                    .Select(x => new BlokPrikazVM.BlokPrikazInfo()
                    {
                        Id = x.BlokID,
                        Grad = x.Grad,
                        Naziv = x.Naziv,
                        BrojNarucioca = context.Narucioci.Where(s => s.BlokID == x.BlokID).Count(),
                        Narucioci = context.Narucioci.Where(s => s.BlokID == x.BlokID).ToList()
                    })
                    .ToList()
            };
        }

        public IActionResult Dodaj(int? gradid)
        {
            return PartialView("Uredi", new BlokUrediVM
            {
                Gradovi = SviGradovi(),
                GradID = gradid != null ? (int)gradid : 0,
                PredefinedGrad = gradid != null ? true : false
            });
        }

        public IActionResult Uredi(int id)
        {
            Blok x = context.Blokovi.Find(id);
            return PartialView(new BlokUrediVM
            {
                Id = x.BlokID,
                Naziv = x.Naziv,
                GradID = x.GradID,
                Gradovi = SviGradovi(),
            });
        }

        public IActionResult Snimi(BlokUrediVM Model)
        {
            Blok blok;
            if (Model.Id == 0)
            {
                blok = new Blok();
                context.Blokovi.Add(blok);
            }
            else
            {
                blok = context.Blokovi.Find(Model.Id);
            }

            blok.Naziv = Model.Naziv;
            blok.GradID = Model.GradID;

            context.SaveChanges();

            if (Model.PredefinedGrad)
            {
                return RedirectToAction("IndexPartial", "Grad", new { area = "AdminModul" });
            }

            return PartialView("Index", GetAllBlokoviVM());
        }

        public IActionResult Obrisi(int id)
        {
            Blok x = context.Blokovi.Find(id);

            context.Blokovi.Remove(x);
            context.SaveChanges();

            return PartialView("Index", GetAllBlokoviVM());
        }

        private List<SelectListItem> SviGradovi()
        {
            List<SelectListItem> Gradovi = new List<SelectListItem>();
            Gradovi.AddRange(context.Gradovi.Select(x => new SelectListItem
            {
                Value = x.GradID.ToString(),
                Text =  x.PoštanskiBroj + " " +  x.Naziv
            }));
            return Gradovi;
        }
    }
}