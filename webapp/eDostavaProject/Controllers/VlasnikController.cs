using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.ViewModels;
using eDostava.Data.Models;
using RS1_Ispit_2017.Helper;

namespace eDostava.Web.Controllers
{
    public class VlasnikController : Controller
    {
        private MojContext context;
        public VlasnikController(MojContext db)
        {
            context = db;
        }
        public IActionResult Index()
        {

            if (HttpContext.GetLogiranogModeratora() == null)
                return RedirectToAction("Index", "Login");

            VlasnikPrikazVM Model = new VlasnikPrikazVM();
            Model.Vlasnici = context.Vlasnici
                .Select(x => new VlasnikPrikazVM.VlasnikInfo
                {
                    VlasnikId = x.KorisnikID,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Username = x.Prezime,
                    Password = x.Password,
                    Email = x.Email,
                    DatumKreiranja = x.DatumKreiranja.ToString(),
                    Restorani = context.Restorani.Where(t => t.VlasnikID == x.KorisnikID).ToList(),
                    UkupnoRestorana = context.Restorani.Where(t => t.VlasnikID == x.KorisnikID).Count()
                })
                .ToList();
            return View(Model);
        }

        public IActionResult Dodaj()
        {
            return View("Uredi", new VlasnikUrediVM());
        }
        public IActionResult Obrisi(int Id)
        {
            Vlasnik x = context.Vlasnici.Find(Id);
            context.Remove(x);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Uredi(int Id)
        {
            Vlasnik x = context.Vlasnici.Find(Id);
            return View(new VlasnikUrediVM
            {
                VlasnikId = x.KorisnikID,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Email = x.Email,
                Username = x.Username,
                Password = x.Password,
                DatumKreiranja = x.DatumKreiranja.ToString()
            });
        }
        public IActionResult Snimi(VlasnikUrediVM model)
        {
            Vlasnik vlasnik;
            if (model.VlasnikId == 0)
            {
                vlasnik = new Vlasnik();
                context.Vlasnici.Add(vlasnik);
            } else
            {
                vlasnik = context.Vlasnici.Find(model.VlasnikId);
            }

            vlasnik.KorisnikID = model.VlasnikId;
            vlasnik.Ime = model.Ime;
            vlasnik.Prezime = model.Prezime;
            vlasnik.Username = model.Username;
            vlasnik.Password = model.Password;
            vlasnik.Email = model.Email;
            vlasnik.DatumKreiranja = Convert.ToDateTime(model.DatumKreiranja);

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}