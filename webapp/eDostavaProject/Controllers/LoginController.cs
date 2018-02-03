using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
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

            HttpContext.Session.Clear();
            return View();
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
    }
}