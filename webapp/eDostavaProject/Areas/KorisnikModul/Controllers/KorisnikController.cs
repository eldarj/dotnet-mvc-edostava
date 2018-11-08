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

        public IActionResult Index(bool narudzbe)
        {
            ViewBag.RenderNarudzbe = narudzbe;
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
                Telefon = n.Telefon,
                Email = n.Email,
                BadgeId = n.BadgeID,
                BlokId = n.BlokID,
                Blok = Blokovi(),
            });
        }

        public IActionResult Snimi(ProfilVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Profil");
            }

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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Lozinka");
            }

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
            List<SelectListItem> Blokovi = new List<SelectListItem>();
            Blokovi.AddRange(context.Blokovi.Select(x => new SelectListItem { Value = x.BlokID.ToString(), Text = x.Grad.Naziv + ", " + x.Naziv }));
            return Blokovi;
        }
    }
}