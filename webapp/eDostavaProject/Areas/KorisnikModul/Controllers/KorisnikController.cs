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
    [Area("KorisnikModul")]
    public class KorisnikController : Controller
    {
        private MojContext context;
        public KorisnikController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Profil()
        {
            Narucilac n = context.Narucioci.Where(s => s.KorisnikID == HttpContext.GetLogiranogNarucioca().KorisnikID).First();
            return PartialView(new ProfilVM
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
            });
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
            narucilac.Email = model.Email;
            narucilac.DatumKreiranja = Convert.ToDateTime(model.DatumKreiranja);
            narucilac.BlokID = (int)model.BlokId;

            context.SaveChanges();
            return RedirectToAction("Profil");
        }

        public IActionResult Lozinka()
        {
            Narucilac n = context.Narucioci.Where(s => s.KorisnikID == HttpContext.GetLogiranogNarucioca().KorisnikID).First();
            return PartialView(new ProfilLozinkaVM
            {
                KorisnikID = n.KorisnikID
            });
        }

        public IActionResult SnimiLozinku(ProfilLozinkaVM model)
        {
            Narucilac narucilac = context.Narucioci.Find(model.KorisnikID);

            if (model.Password == narucilac.Password && model.NoviPassword == model.NoviPasswordPonovo)
            {
                narucilac.Password = model.NoviPassword;
                HttpContext.SetLogiranogNarucioca(narucilac);
                context.SaveChanges();
            }
            return RedirectToAction("Profil");
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult ProvjeriLozinku(string Password)
        {
            Narucilac narucilac = HttpContext.GetLogiranogNarucioca();

            if (narucilac.Password != Password)
            {
                return NotFound();
            }

            return Ok();
        }

        private List<SelectListItem> Blokovi()
        {
            var blokovi = new List<SelectListItem>();
            blokovi.Add(new SelectListItem { Value = null, Text = "Izaberite blok" });
            blokovi.AddRange(context.Blokovi.Select(x => new SelectListItem { Value = x.BlokID.ToString(), Text = x.Grad.Naziv + ", " + x.Naziv }));
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