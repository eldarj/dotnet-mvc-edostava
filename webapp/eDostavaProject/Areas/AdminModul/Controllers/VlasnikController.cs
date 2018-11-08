using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.Areas.AdminModul.ViewModels;
using eDostava.Data.Models;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    [Area("AdminModul")]
    public class VlasnikController : Controller
    {
        private MojContext context;
        public VlasnikController(MojContext db)
        {
            context = db;
        }
        public IActionResult Index()
        {
            return View(GetAllVlasniciVM());
        }

        public IActionResult IndexPartial()
        {
            return PartialView("Index", GetAllVlasniciVM());
        }

        private VlasnikPrikazVM GetAllVlasniciVM()
        {
            return new VlasnikPrikazVM
            {
                Vlasnici = context.Vlasnici
                .Select(x => new VlasnikPrikazVM.VlasnikInfo
                {
                    Id = x.KorisnikID,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Username = x.Prezime,
                    Email = x.Email,
                    DatumKreiranja = x.DatumKreiranja,
                    Restorani = context.Restorani.Where(t => t.VlasnikID == x.KorisnikID).ToList(),
                    UkupnoRestorana = context.Restorani.Where(t => t.VlasnikID == x.KorisnikID).Count()
                })
                .ToList()
            };
        }

        public IActionResult Dodaj()
        {
            return PartialView("Uredi", new VlasnikUrediVM());
        }

        public IActionResult Uredi(int id)
        {
            Vlasnik x = context.Vlasnici.Find(id);

            return PartialView(new VlasnikUrediVM
            {
                Id = x.KorisnikID,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Email = x.Email,
                Username = x.Username
            });
        }

        public IActionResult Snimi(VlasnikUrediVM Model)
        {
            Vlasnik vlasnik;
            if (Model.Id == 0)
            {
                vlasnik = new Vlasnik();
                context.Vlasnici.Add(vlasnik);
            }
            else
            {
                vlasnik = context.Vlasnici.Find(Model.Id);
            }

            vlasnik.Ime = Model.Ime;
            vlasnik.Prezime = Model.Prezime;
            vlasnik.Username = Model.Username;
            vlasnik.Email = Model.Email;
            vlasnik.DatumKreiranja = DateTime.Now;

            context.SaveChanges();

            return PartialView("Index", GetAllVlasniciVM());
        }

        public IActionResult Obrisi(int id)
        {
            Vlasnik x = context.Vlasnici.Find(id);

            context.Vlasnici.Remove(x);
            context.SaveChanges();

            return PartialView("Index", GetAllVlasniciVM());
        }
    }
}