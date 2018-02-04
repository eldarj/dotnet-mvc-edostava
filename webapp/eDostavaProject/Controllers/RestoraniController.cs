using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Web.ViewModels;
using eDostava.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using eDostava.Data.Models;
using RS1_Ispit_2017.Helper;

namespace eDostava.Web.Controllers
{
    public class RestoraniController : Controller
    {
        private MojContext context;
        public RestoraniController(MojContext db)
        {
            context = db;
        }
        public IActionResult Index()

        {

            
            if (HttpContext.GetLogiranogNarucioca()==null && HttpContext.GetLogiranogVlasnika() == null && HttpContext.GetLogiranogModeratora() == null)
                return RedirectToAction("Index", "Login");


            RestoranIndexVM model = new RestoranIndexVM();
            model.Rows = context.Restorani.Include(x=>x.Vlasnik).Include(x => x.Blok).Include(x => x.Blok.Grad).Select(x => new RestoranIndexVM.Row
            {
                nazivRestorana=x.Naziv,
                RestoranID = x.RestoranID,
                brojTelefona = x.Telefon,
                lokacijaRestorana = x.Blok.Naziv + ", " + x.Blok.Grad.Naziv,
                minimalnaCijenaNarudzbe = x.MinimalnaCijenaNarudžbe,
                brojLajkova = context.Lajkovi.Where(y => y.RestoranID == x.RestoranID).Count(),
                opis = x.Opis,
                vlasnik=x.Vlasnik.Ime_prezime,
                radnoVrijeme = context.VrijemeRada.Where(y => y.RestoranID == x.RestoranID).Select(y => new SelectListItem
                {
                    Value= ((Dani)y.Dan).ToString() + " : " + y.VrijemeOtvaranja.ToString() + " - " + y.VrijemeZatvaranja.ToString()
        }).ToList()
            }).ToList();



            if (HttpContext.GetLogiranogModeratora() != null)
            {
                model.jeLogiran = (RestoranIndexVM.Logiran)0;
            }

            if (HttpContext.GetLogiranogVlasnika() != null)
            {
                Vlasnik n = HttpContext.GetLogiranogVlasnika();
                model.jeLogiran = (RestoranIndexVM.Logiran)1;
                model.vlasnik = n;
            }

            if (HttpContext.GetLogiranogNarucioca() != null)
            {
                model.jeLogiran = (RestoranIndexVM.Logiran)2;
            }


            return View(model);
        }

        
        public IActionResult Jelovnik(int restoranid)
        {
            JelovnikVM model = new JelovnikVM();
            model.Rows = context.Jelovnici.Where(x => x.RestoranID == restoranid).Select(x => new JelovnikVM.Row
            {
                JelovnikID= x.JelovnikID,
                isAktivan=x.Aktivan,
                OpisJelovnika=x.Opis
            }).ToList();

            model.minimalanIznos = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.MinimalnaCijenaNarudžbe).FirstOrDefault();
            model.nazivRestorana = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.Naziv).FirstOrDefault();
            model.lokacija = context.Restorani.Include(x=>x.Blok).Include(x=>x.Blok.Grad).Where(x => x.RestoranID == restoranid).Select(x => x.Blok.Naziv + ", " + x.Blok.Grad.Naziv).FirstOrDefault();
            model.lajkovi = context.Lajkovi.Where(x => x.RestoranID == restoranid).Count();
            model.vlasnik = context.Restorani.Include(x=>x.Vlasnik).Where(x => x.RestoranID == restoranid).Select(x => x.Vlasnik.Ime_prezime).First();
            model.telefon = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.Telefon).FirstOrDefault();
            

            return View(model);
        }


        public IActionResult PrijaviRestoran()
        {
            RestoranPrijavaVM model = new RestoranPrijavaVM();

            
            model.blokovi = context.Blokovi.Include(x=>x.Grad).Select(x => new SelectListItem
            {
                Text = x.Naziv + ", " + x.Grad.Naziv,
                Value = x.BlokID.ToString()

            }).ToList();

            model.vlasnici = context.Vlasnici.Select(x => new SelectListItem
            {
                Text = x.Ime_prezime,
                Value = x.KorisnikID.ToString()

            }).ToList();

            return View(model);
        }

        public IActionResult SnimiRestoran(RestoranPrijavaVM model)
        {
            Restoran n = new Restoran
            {
                BlokID=model.blokId,
                MinimalnaCijenaNarudžbe=model.minimalnaCijenaNarudzbe,
                Naziv=model.naziv,
                Opis=model.opis,
                Telefon=model.brojTelefona,
                VlasnikID=model.vlasnikId,
                
            };
            context.Restorani.Add(n);
            context.SaveChanges();
            HttpContext.SetLogiranogModeratora(HttpContext.GetLogiranogModeratora());
            return RedirectToAction("Index");
        }
    }
}