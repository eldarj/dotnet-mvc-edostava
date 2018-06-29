using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_2017.Helper;

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