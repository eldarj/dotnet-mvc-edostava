using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data.Models;
using eDostava.Data;
using RS1_Ispit_2017.Helper;
using eDostava.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eDostava.Web.Controllers
{
    public class KorisnikController : Controller
    {
        private MojContext context;
        public KorisnikController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            return View("Index");
        }

        public IActionResult Profil()
        {
            Narucilac n = context.Narucioci.Where(s => s.KorisnikID == HttpContext.GetLogiranogNarucioca().KorisnikID).First();

            ProfilVM profil = new ProfilVM
            {
                KorisnikID = n.KorisnikID,
                Ime = n.Ime,
                Prezime = n.Prezime,
                Username = n.Username,
                Password = n.Password,
                Telefon = n.Telefon,
                Email = n.Email,
                BadgeId = n.BadgeID,
                Badge = Badgevi(),
                BlokId = n.BlokID,
                Blok = Blokovi(),
            };


            return PartialView(profil);
        }

        public IActionResult Snimi(ProfilVM model)
        {
            Narucilac narucilac;
            if (model.KorisnikID == 0)
            {
                narucilac = new Narucilac();
                context.Narucioci.Add(narucilac);
            }
            else
            {
                narucilac = context.Narucioci.Find(model.KorisnikID);
            }

            narucilac.Ime = model.Ime;
            narucilac.Prezime = model.Prezime;
            narucilac.Telefon = model.Telefon;
            narucilac.Username = model.Username;
            narucilac.Password = model.Password;
            narucilac.Email = model.Email;
            narucilac.DatumKreiranja = Convert.ToDateTime(model.DatumKreiranja);
            narucilac.BadgeID = (int) model.BadgeId;
            narucilac.BlokID = (int)model.BlokId;

            context.SaveChanges();
            return RedirectToAction("Profil");
        }

        private List<SelectListItem> Blokovi()
        {
            var blokovi = new List<SelectListItem>();
            blokovi.Add(new SelectListItem { Value = null, Text = "Izaberite blok" });
            blokovi.AddRange(context.Blokovi.Select(x => new SelectListItem { Value = x.BlokID.ToString(), Text = x.Naziv }));
            return blokovi;
        }
        private List<SelectListItem> Badgevi()
        {
            var badgevi = new List<SelectListItem>();
            badgevi.Add(new SelectListItem { Value = null, Text = "Izaberite badge" });
            badgevi.AddRange(context.Badgevi.Select(x => new SelectListItem { Value = x.BadgeID.ToString(), Text = x.Naziv }));
            return badgevi;
        }
    }
}