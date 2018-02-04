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

namespace RS1_Ispit_2017_06_21_v1.Controllers
{
    public class LoginController : Controller
    {
        private MojContext context;
        public LoginController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            BlokLista model = new BlokLista();
             model.blokovi = context.Blokovi.Include(x=>x.Grad).Select(x => new SelectListItem
            {
                Text = x.Naziv + ", " + x.Grad.Naziv,
                Value = x.BlokID.ToString()
            }).ToList();
            HttpContext.Session.Clear();
          
            return View(model);
        }

        public IActionResult Prijava(string username, string password)
        {
            HttpContext.Session.Clear();

            Vlasnik n1 = context.Vlasnici.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            Moderator n2 = context.Moderatori.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
            Narucilac n3 = context.Narucioci.Where(x => x.Username == username && x.Password == password).FirstOrDefault();

            if (n1 == null && n2==null && n3==null)
                return RedirectToAction("Index");

            if(n1!=null)
            HttpContext.SetLogiranogVlasnika(n1);

            if (n2 != null)
                HttpContext.SetLogiranogModeratora(n2);
            if (n3 != null)
                HttpContext.SetLogiranogNarucioca(n3);


            return RedirectToAction("Index", "Restorani");


        }


        public IActionResult Registracija(int blokid, string korisnik,string ime,string prezime,string username,string password,string email, DateTime datumKreiranja)
        {
            if(korisnik=="vlasnik")
            {
                Vlasnik n = new Vlasnik
                {
                    Ime=ime,
                    Prezime=prezime,
                    DatumKreiranja=datumKreiranja,
                    Email=email,
                    Password=password,
                    Username=username
                };

                context.Vlasnici.Add(n);
                context.SaveChanges();
                HttpContext.SetLogiranogVlasnika(n);
                return RedirectToAction("Index","Restorani");
            }
            else
            {
                Narucilac n = new Narucilac
                {
                    Ime = ime,
                    Prezime = prezime,
                    DatumKreiranja = datumKreiranja,
                    Email = email,
                    Password = password,
                    Username = username,
                    BlokID=blokid,
                    BadgeID=1
                    
                   
                };

                context.Narucioci.Add(n);
                context.SaveChanges();
                HttpContext.SetLogiranogNarucioca(n);
                return RedirectToAction("Index", "Restorani");
            }


        }


        
    }
}