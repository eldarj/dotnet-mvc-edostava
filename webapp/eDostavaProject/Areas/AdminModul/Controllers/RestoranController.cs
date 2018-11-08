using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.Areas.AdminModul.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using eDostava.Web.Areas.AdminModul.Helper;
using Microsoft.AspNetCore.Authorization;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    public class RestoranController : AdminController
    {
        private MojContext context;
        private IHostingEnvironment _appEnvironment;
        private readonly string UploadFolder = "uploads/images/restoran";

        public RestoranController (MojContext db, IHostingEnvironment hostingEnvironment)
        {
            context = db;
            _appEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View(PrepareAllRestorani());
        }

        private RestoranPrikazVM PrepareAllRestorani()
        {
            return new RestoranPrikazVM
            {
                Restorani = context.Restorani
                    .Select(x => new RestoranPrikazVM.RestoranInfo
                    {
                        Id = x.RestoranID,
                        Naziv = x.Naziv,
                        Opis = x.Opis,
                        Vlasnik = x.Vlasnik,
                        BrojTelefona = x.Telefon,
                        Lokacija = x.Blok.Grad.Naziv + ", " + x.Blok.Naziv,
                        BrojLajkova = context.Lajkovi.Where(l => l.RestoranID == x.RestoranID).Count(),
                        Slika = x.Slika,
                        Slogan = x.Slogan,
                        WebUrl = x.WebUrl
                    })
                    .ToList()
            };
        }

        public IActionResult Dodaj(int? vlasnikid)
        {
            return PartialView("Uredi", new RestoranUrediVM
            {
                Blokovi = SviBlokovi(),
                Vlasnici = SviVlasnici(),
                VlasnikId = vlasnikid != null ? (int)vlasnikid : 0,
                PredefinedVlasnik = vlasnikid != null ? true : false
            });
        }

        public IActionResult Uredi(int id)
        {
            Restoran x = context.Restorani.Find(id);
            return PartialView(new RestoranUrediVM
            {
                Id = x.RestoranID,
                Naziv = x.Naziv,
                Opis = x.Opis,
                Slogan = x.Slogan,
                Telefon = x.Telefon,
                WebUrl = x.WebUrl,
                SlikaPath = x.Slika,
                VlasnikId = x.VlasnikID,
                Vlasnici = SviVlasnici(),
                BlokId = x.BlokID,
                Blokovi = SviBlokovi(),
            });
        }

        public IActionResult Snimi(RestoranUrediVM Model)
        {
            if (!ModelState.IsValid)
            {
                Model.Vlasnici = SviVlasnici();
                Model.Blokovi = SviBlokovi();
                return PartialView("Uredi", Model);
            }

            Restoran restoran;
            if (Model.Id == 0)
            {
                restoran = new Restoran();
                context.Restorani.Add(restoran);
            }
            else
            {
                restoran = context.Restorani.Find(Model.Id);
            }

            restoran.Naziv = Model.Naziv;
            restoran.Opis = Model.Opis;
            restoran.Telefon = Model.Telefon;
            restoran.WebUrl = Model.WebUrl;
            restoran.Slogan = Model.Slogan;

            restoran.VlasnikID = Model.VlasnikId;
            restoran.BlokID = Model.BlokId;

            // Provjeri je li imamo uploadovanu sliku
            if (Model.Slika != null)
            {
                string Filename = GetUniqueFileName(Model.Slika.FileName);
                string Uploads = Path.Combine(_appEnvironment.WebRootPath, UploadFolder);
                string FilePath = Path.Combine(Uploads, Filename); // Pripremi path i ime slike
                Model.Slika.CopyTo(new FileStream(FilePath, FileMode.Create)); // Uploaduj filestream (content/sliku)
                
                restoran.Slika = UploadFolder + "/" + Filename;
            }

            context.SaveChanges();

            if (Model.PredefinedVlasnik)
            {
                return RedirectToAction("IndexPartial", "Vlasnik", new { area = "AdminModul" });
            }

            return PartialView("Index", PrepareAllRestorani());
        }

        public IActionResult Obrisi(int id)
        {
            Restoran x = context.Restorani.Find(id);

            context.Restorani.Remove(x);
            context.SaveChanges();

            return PartialView("Index", PrepareAllRestorani());
        }

        private List<SelectListItem> SviBlokovi()
        {
            List<SelectListItem> Blokovi = new List<SelectListItem>();
            Blokovi.AddRange(context.Blokovi.Select(x => new SelectListItem
            {
                Value = x.BlokID.ToString(),
                Text = x.Naziv + " (" + x.Grad.PoštanskiBroj + " " + x.Grad.Naziv + ")"
            }));
            return Blokovi;
        }

        private List<SelectListItem> SviVlasnici()
        {
            List<SelectListItem> Vlasnici = new List<SelectListItem>();
            Vlasnici.AddRange(context.Vlasnici.Select(x => new SelectListItem
            {
                Value = x.KorisnikID.ToString(),
                Text = x.Ime_prezime
            }));
            return Vlasnici;
        }
        private string GetUniqueFileName(string Filename)
        {
            Filename = Path.GetFileName(Filename);

            // Escape invalid kraktere
            foreach (var inv in Path.GetInvalidFileNameChars())
            {
                Filename = Filename.Replace(inv, '-');
            }

            return Path.GetFileNameWithoutExtension(Filename)
                    + "_" + Guid.NewGuid().ToString().Substring(0, 4)
                    + Path.GetExtension(Filename);
        }

        public IActionResult ProvjeriNaziv(string Naziv, int BlokId)
        {
            Blok blok = context.Blokovi.Find(BlokId);
            if (!context.Restorani.Any(x => x.Naziv == Naziv && x.BlokID == BlokId))
            {
                return Json($"Već postoji restoran sa istim nazivom u " +
                    $"istom bloku! Restoran {Naziv}, blok {blok.Naziv}, {blok.Grad.Naziv}");
            }
            return Json(true);
        }
    }
}