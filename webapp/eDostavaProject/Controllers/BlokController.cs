using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.ViewModels;

namespace eDostava.Web.Controllers
{
    public class BlokController : Controller
    {
        private MojContext context;
        public BlokController (MojContext db)
        {
            context = db;
        }
        public IActionResult Index(int? gradId = null)
        {
            BlokPrikazVM Model = new BlokPrikazVM();
            Model.GradId =  gradId;
            Model.Blokovi = context.Blokovi
                .Select(x => new BlokPrikazVM.BlokPrikazInfo()
                {
                    BlokId = x.BlokID,
                    GradId = x.GradID,
                    Grad = x.Grad,
                    Naziv = x.Naziv,
                    BrojNarucioca = context.Narucioci.Where(s=>s.BlokID == x.BlokID).Count()
                })
                .ToList();
            if (gradId != null)
            {
                Model.Blokovi = Model.Blokovi.Where(x => x.GradId == gradId).ToList();
                return PartialView(Model);
            }
            return View(Model);
        }
        public IActionResult Dodaj(int gradId)
        {
            BlokDodajVM Model = new BlokDodajVM();
            Model.GradId = gradId;

            return PartialView("Uredi", Model);
        }
        public IActionResult Uredi(int blokId)
        {
            Blok x = context.Blokovi.Find(blokId);
            return PartialView(new BlokDodajVM
            {
                GradId = x.GradID,
                BlokId = x.BlokID,
                nazivBloka = x.Naziv
            });
        }
        public IActionResult Snimi(BlokDodajVM model)
        {
            Blok blok;
            if (model.BlokId == 0)
            {
                blok = new Blok
                {
                    GradID = model.GradId
                };
                context.Blokovi.Add(blok);
            }
            else
            {
                blok = context.Blokovi.Find(model.BlokId);
            }
            blok.Naziv = model.nazivBloka;

            context.SaveChanges();
            return Redirect("/Blok/Index?gradId=" + model.GradId);
        }

        public IActionResult Obrisi(int blokId)
        {
            Blok x = context.Blokovi.Find(blokId);
            int gradId = x.GradID;
            context.Blokovi.Remove(x);
            context.SaveChanges();

            return Redirect("/Blok/Index?gradId=" + gradId);
        }
    }
}