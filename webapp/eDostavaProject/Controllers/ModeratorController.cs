using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eDostava.Web.Controllers
{
    public class ModeratorController : Controller
    {
        private MojContext context;
        public ModeratorController(MojContext db)
        {
            context = db;
        }
        public IActionResult Narucioci()
        {
            NarucilacPrikazVM Model = new NarucilacPrikazVM();
            Model.Narucioci = context.Narucioci.Select(x => new NarucilacPrikazVM.NaruciociInfo()
            {
                Narucilac = x,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email,
                Telefon = x.Telefon,
                DatumKreiranja = x.DatumKreiranja.ToString(),
                BlokNaziv = x.Blok.Naziv,
                BadgeNaziv = x.Badge.Naziv,
                GradNaziv = x.Blok.Grad.Naziv,
                PostanskiBroj = x.Blok.Grad.PoštanskiBroj,
            })
            .ToList();

            return View(Model);
        }
        public IActionResult Index()
        {
            return View();
        }
    }

}