using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.AdminModul.ViewModels;
using Microsoft.EntityFrameworkCore;
using eDostava.Web.Areas.AdminModul.Helper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    public class NarucilacController : AdminController
    {
        private MojContext context;
        public NarucilacController(MojContext db)
        {
            context = db;
        }
        public IActionResult Index()
        {
            return View(PrepareAllNarucioce());
        }

        private NarucilacPrikazVM PrepareAllNarucioce()
        {
            return new NarucilacPrikazVM()
            {
                Narucioci = context.Narucioci
                .Select(x => new NarucilacPrikazVM.NaruciociInfo()
                {
                    Id = x.KorisnikID,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Username = x.Username,
                    Email = x.Email,
                    Telefon = x.Telefon,
                    DatumKreiranja = x.DatumKreiranja,
                    BlokNaziv = x.Blok.Naziv,
                    Grad = x.Blok.Grad,
                    PostanskiBroj = x.Blok.Grad.PoštanskiBroj,
                    UkupnoNarudzbi = x.Narudzbe.Count()
                })
                .ToList()
            };
        }

        public IActionResult Dodaj()
        {
            return PartialView("Uredi", new NarucilacUrediVM
            {
                Blokovi = SviBlokovi()
            });
        }

        public IActionResult Uredi(int id)
        {
            Narucilac x = context.Narucioci.Find(id);

            return PartialView(new NarucilacUrediVM
            {
                Id = x.KorisnikID,
                Ime = x.Ime,
                Prezime = x.Prezime,
                Telefon = x.Telefon,
                Username = x.Username,
                Email = x.Email,
                BlokID = x.BlokID,
                Blokovi = SviBlokovi(),
            });
        }

        public IActionResult Snimi(NarucilacUrediVM Model)
        {
            if (!ModelState.IsValid)
            {
                Model.Blokovi = SviBlokovi();
                return PartialView("Uredi", Model);
            }

            Narucilac narucilac;
            if (Model.Id == 0)
            {
                narucilac = new Narucilac();
                context.Narucioci.Add(narucilac);
            }
            else
            {
                narucilac = context.Narucioci.Find(Model.Id);
            }

            narucilac.Ime = Model.Ime;
            narucilac.Prezime = Model.Prezime;
            narucilac.Telefon = Model.Telefon;
            narucilac.Username = Model.Username;
            narucilac.Email = Model.Email;
            narucilac.BadgeID = 1;
            narucilac.DatumKreiranja = DateTime.Now;
            narucilac.BlokID = Model.BlokID;

            context.SaveChanges();

            return PartialView("Index", PrepareAllNarucioce());
        }

        public IActionResult Obrisi(int id)
        {
            Narucilac n = context.Narucioci.Find(id);
            context.Narucioci.Remove(n);
            context.SaveChanges();

            return PartialView("Index", PrepareAllNarucioce());
        }

        public IActionResult Detaljno(int id)
        {
            Narucilac n = context.Narucioci
                .Include(x => x.Narudzbe)
                .Include(x => x.Blok)
                .ThenInclude(x => x.Grad)
                .Where(x => x.KorisnikID == id)
                .FirstOrDefault();

            return PartialView(new NarucilacDetaljnoVM
            {
                Ime = n.Ime,
                Prezime = n.Prezime,
                Username = n.Username,
                Email = n.Email,
                Telefon = n.Telefon,
                DatumKreiranja = n.DatumKreiranja.ToString(),
                GradNaziv = n.Blok.Grad.Naziv,
                BlokNaziv = n.Blok.Naziv,
                PostanskiBroj = n.Blok.Grad.PoštanskiBroj,
                UkupnoNarudzbi = n.Narudzbe.Count,
                UkupnoPotroseno = n.Narudzbe.Select(s => s.UkupnaCijena).Sum()
            });
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
    }
}