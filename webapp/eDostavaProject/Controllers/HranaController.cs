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


        public IActionResult Index(int jelovnikid)
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

            Vlasnik n = HttpContext.GetLogiranogVlasnika();

            if(context.Jelovnici.Include(x=>x.Restoran).Where(x=>x.JelovnikID==jelovnikid).Where(x=>x.Restoran.VlasnikID==n.KorisnikID).SingleOrDefault()!=null)
            {
                model.jeLogiranVlasnikRestorana = true;
            }
            return View(model);
        }

       [HttpGet]
        public IActionResult UrediProizvod(int proizvodid, int jelovnikid)
        {
            HranaUrediVM model = new HranaUrediVM();


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
            return RedirectToAction("UrediRestoran", "Restorani", new { restoranid = restoranid });
        }
    }

    
   
}