using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_Ispit_2017.Helper;
using eDostava.Web.ViewModels;

namespace eDostava.Web.Controllers
{
   
    public class NarucilacController : Controller
    {
        private MojContext context;
        public NarucilacController(MojContext db)
        {
            context = db;
        }
        public IActionResult Index(int? GradId)
        {       
            NarucilacPrikazVM Model = new NarucilacPrikazVM();
            Model.Narucioci = context.Narucioci
            .Select(x => new NarucilacPrikazVM.NaruciociInfo()
            {
                Narucilac = x,
                NarucilacId = x.KorisnikID,
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
                Grad = x.Blok.Grad,
                PostanskiBroj = x.Blok.Grad.PoštanskiBroj,
            })
            .ToList();

            if (GradId != null)
            {
                Model.Grad = context.Gradovi.Where(x => x.GradID == GradId).FirstOrDefault();
                Model.Narucioci = Model.Narucioci.Where(x => x.Grad.GradID == GradId).ToList();
            }

            return View(Model);
        }


        public IActionResult Dodaj()
        {
            return View("Uredi", new NarucilacUrediVM
            {
                Blokovi = SviBlokovi(),
                Badgevi = SviBadgevi()
            });
        }
        public IActionResult Obrisi(int Id)
        {
            Narucilac x = context.Narucioci.Find(Id);
            context.Narucioci.Remove(x);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Uredi(int Id)
        {
            Narucilac x = context.Narucioci.Find(Id);
            return View(new NarucilacUrediVM
            {
                NarucilacID = x.KorisnikID,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Telefon = x.Telefon,
                Username = x.Username,
                Password = x.Password,
                Email = x.Email,
                DatumKreiranja = x.DatumKreiranja.ToString(),
                BlokID = x.BlokID,
                BadgeID = x.BadgeID,
                //EJ: Mislim da nam fali veza - napomena: provjeriti vezu
                //UkupnoNarudzbi = context.Narudzbe.Where(x => x).Count();
                Blokovi = SviBlokovi(),
                Badgevi = SviBadgevi()
            });
        }
        public IActionResult Snimi(NarucilacUrediVM model)
        {

            Narucilac narucilac;
            if (model.NarucilacID == 0)
            {
                narucilac = new Narucilac();
                context.Narucioci.Add(narucilac);
            }
            else
            {
                narucilac = context.Narucioci.Find(model.NarucilacID);
            }

            narucilac.Ime = model.Ime;
            narucilac.Prezime = model.Prezime;
            narucilac.Telefon = model.Telefon;
            narucilac.Username = model.Username;
            narucilac.Password = model.Password;
            narucilac.Email = model.Email;
            narucilac.DatumKreiranja = Convert.ToDateTime(model.DatumKreiranja);
            narucilac.BadgeID = model.BadgeID;
            narucilac.BlokID = model.BlokID;

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        private List<SelectListItem> SviBlokovi()
        {
            var blokovi = new List<SelectListItem>();
            blokovi.AddRange(context.Blokovi.Select(x => new SelectListItem
            {
                Value = x.BlokID.ToString(),
                Text = x.Naziv + " (" + x.Grad.Naziv + ")"
            }));
            return blokovi;
        }
        private List<SelectListItem> SviBadgevi()
        {
            var badgevi = new List<SelectListItem>();
            badgevi.AddRange(context.Badgevi.Select(x => new SelectListItem
            {
                Value = x.BadgeID.ToString(),
                Text = x.Naziv
            }));
            return badgevi;
        }

        public IActionResult LikeRestoran(int restoranid)
        {

            if (HttpContext.GetLogiranogNarucioca() != null)
            {
                int narucilacid = HttpContext.GetLogiranogNarucioca().KorisnikID;
                RestoranLike n = new RestoranLike
                {
                    NarucilacID = narucilacid,
                    RestoranID = restoranid,
                };
                context.Lajkovi.Add(n);
                context.SaveChanges();

                return RedirectToAction("Index", "Restorani");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
             
            
        }

        public IActionResult UnlikeRestoran(int restoranid)
        {

            if (HttpContext.GetLogiranogNarucioca() != null)
            {
                int narucilacid = HttpContext.GetLogiranogNarucioca().KorisnikID;
              
                RestoranLike n = context.Lajkovi.Where(x => x.RestoranID == restoranid && x.NarucilacID == narucilacid).FirstOrDefault();
                context.Remove(n);
                context.SaveChanges();

                return RedirectToAction("Index", "Restorani");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}