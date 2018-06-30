using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RS1_Ispit_2017.Helper;

namespace eDostava.Web.Controllers
{
    public class HranaController : Controller
    {
        private MojContext context;
        public HranaController(MojContext db)
        {
            context = db;
        }

        internal static object ValidacijaNaziv()
        {
            throw new NotImplementedException();
        }

        public IActionResult Index(int jelovnikid,int divid)
        {
            HranaIndexVM model = new HranaIndexVM();
            model.jelovnikID = jelovnikid;
            model.Rows = context.Proizvodi.Where(x => x.JelovnikID == jelovnikid).Select(x => new HranaIndexVM.Row
            {
                HranaID = x.HranaID,
                cijena = x.Cijena,
                Naziv = x.Naziv,
                opis = x.Opis,
                prilog = x.Prilog,
                tipkuhinjeID = x.TipKuhinjeID
            }).ToList();
            model.DivID = divid;
            model.jeLogiranVlasnikRestorana = false;
            Vlasnik n = HttpContext.GetLogiranogVlasnika();

            if (n != null)
            {
                if (context.Jelovnici.Include(x => x.Restoran).Where(x => x.JelovnikID == jelovnikid).Where(x => x.Restoran.VlasnikID == n.KorisnikID).SingleOrDefault() != null)
                {
                    model.jeLogiranVlasnikRestorana = true;
                }
            }
            return PartialView(model);
        }

       [HttpGet]
        public IActionResult UrediProizvod(int proizvodid, int jelovnikid,int divid)
        {

            
                HranaUrediVM model = new HranaUrediVM();

                model.divID = divid;
                model.HranaID = proizvodid;
                model.Naziv = context.Proizvodi.Where(x => x.HranaID == proizvodid).Select(x => x.Naziv).FirstOrDefault();
                model.cijena = context.Proizvodi.Where(x => x.HranaID == proizvodid).Select(x => x.Cijena).FirstOrDefault();
                model.opis = context.Proizvodi.Where(x => x.HranaID == proizvodid).Select(x => x.Opis).FirstOrDefault();
                model.prilog = context.Proizvodi.Where(x => x.HranaID == proizvodid).Select(x => x.Prilog).FirstOrDefault();
                model.tipoviKuhinje = context.TipoviKuhinje.Select(y => new SelectListItem
                {
                    Text = y.Naziv,
                    Value = y.TipKuhinjeID.ToString()

                }).ToList();
                int restoranid = context.Jelovnici.Where(y => y.JelovnikID == jelovnikid).Select(y => y.RestoranID).FirstOrDefault();
                model.jelovnici = context.Jelovnici.Where(x => x.RestoranID == restoranid).Select(x => new SelectListItem
                {
                    Text = x.Opis,
                    Value = x.JelovnikID.ToString()

                }).ToList();
                model.jelovnikID = jelovnikid;
                model.sifra = context.Proizvodi.Where(x => x.HranaID == proizvodid).Select(x => x.Sifra).FirstOrDefault();
                model.tipkuhinjeID = context.Proizvodi.Where(x => x.HranaID == proizvodid).Select(x => x.TipKuhinjeID).FirstOrDefault();
                return View(model);
            
        }

        [HttpPost]
        public IActionResult UrediProizvod(HranaUrediVM model)
        {
            if (ModelState.IsValid)
            {
                Hrana n = context.Proizvodi.Where(x => x.HranaID == model.HranaID).FirstOrDefault();
                n.Cijena = model.cijena;
                n.JelovnikID = model.jelovnikID;
                n.Naziv = model.Naziv;
                n.Opis = model.opis;
                n.Prilog = model.prilog;
                n.TipKuhinjeID = model.tipkuhinjeID;
                n.Sifra = model.sifra;
                context.Proizvodi.Update(n);
                context.SaveChanges();
                int restoranid = context.Jelovnici.Where(x => x.JelovnikID == model.jelovnikID).Select(x => x.RestoranID).FirstOrDefault();
                HttpContext.SetLogiranogVlasnika(HttpContext.GetLogiranogVlasnika());
                //return RedirectToAction("UrediRestoran", "Restorani", new { restoranid = restoranid });
                return RedirectToAction(nameof(Index), new { jelovnikid = model.jelovnikID, divid = model.divID });
            }
            else
            {
                model.tipoviKuhinje = context.TipoviKuhinje.Select(y => new SelectListItem
                {
                    Text = y.Naziv,
                    Value = y.TipKuhinjeID.ToString()

                }).ToList();
                int restoranid = context.Jelovnici.Where(y => y.JelovnikID == model.jelovnikID).Select(y => y.RestoranID).FirstOrDefault();
                model.jelovnici = context.Jelovnici.Where(x => x.RestoranID == restoranid).Select(x => new SelectListItem
                {
                    Text = x.Opis,
                    Value = x.JelovnikID.ToString()

                }).ToList();
                model.jelovnikID = model.jelovnikID;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult DodajProizvod(int jelovnikid, int divid)
        {
            HranaDodajVM model = new HranaDodajVM();
            model.divID = divid;
            model.tipoviKuhinje = context.TipoviKuhinje.Select(x => new SelectListItem
            {
                Text=x.Naziv,
                Value=x.TipKuhinjeID.ToString()
            }).ToList();

            model.jelovnikID = jelovnikid;
            model.restoranID = context.Jelovnici.Where(x => x.JelovnikID == jelovnikid).Select(x => x.RestoranID).FirstOrDefault();
            return PartialView(model);
        }
        [HttpPost]
        public IActionResult DodajProizvod(HranaDodajVM model)
        {
            if (ModelState.IsValid)
            {
                Hrana n = new Hrana
                {
                    Cijena = model.cijena,
                    Naziv = model.Naziv,
                    Opis = model.opis,
                    Prilog = model.prilog,
                    Sifra = model.sifra,
                    JelovnikID = model.jelovnikID,
                    TipKuhinjeID = model.tipkuhinjeID

                };

                context.Proizvodi.Add(n);
                context.SaveChanges();

                return RedirectToAction(nameof(Index),new {jelovnikid=model.jelovnikID,divid=model.divID });
            }
            else
            {
                model.tipoviKuhinje = context.TipoviKuhinje.Select(x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.TipKuhinjeID.ToString()
                }).ToList();

                model.jelovnikID = model.jelovnikID;
                model.restoranID = context.Jelovnici.Where(x => x.JelovnikID == model.jelovnikID).Select(x => x.RestoranID).FirstOrDefault();

                return View(model);
            }
        }

        public IActionResult ObrisiProizvod(int proizvodid,int jelovnikid,int divid)
        {
            Hrana n = context.Proizvodi.Where(x => x.HranaID == proizvodid).FirstOrDefault();
            context.Proizvodi.Remove(n);
            context.SaveChanges();


            return RedirectToAction(nameof(Index),new { jelovnikid=jelovnikid,divid=divid});
        }
        [HttpGet]
        public IActionResult DodajKuhinju(int jelovnikid,int divid)
        {
            DodajKuhinjuVM model = new DodajKuhinjuVM();
            model.jelovnikid=jelovnikid;
            model.divid = divid;
            return View(model);
        }

        [HttpPost]
        public IActionResult DodajKuhinju(DodajKuhinjuVM model)
        {
            TipKuhinje n = new TipKuhinje {
                Naziv = model.naziv,
                Opis = model.opis
            };
            context.TipoviKuhinje.Add(n);
            context.SaveChanges();
            return RedirectToAction(nameof(DodajProizvod), new { jelovnikid = model.jelovnikid });


        }

        public IActionResult ValidacijaNaziv(string Naziv, int jelovnikID)
        {
            if (context.Proizvodi.Where(x => x.JelovnikID == jelovnikID).Any(x => x.Naziv == Naziv))
            {
                return Json($"Proizvod '{Naziv}' već postoji!");
            }
            return Json(true);
        }

            public IActionResult ValidacijaSifra(int sifra)
            {
                if (context.Proizvodi.Any(x=>x.Sifra==sifra))
                {
                    return Json($"Sifra je već iskorištena");
                }
                return Json(true);
            }
        }

    
   
}