using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Web.Areas.AdminModul.ViewModels;
using eDostava.Data;
using eDostava.Web.Areas.AdminModul.Helper;
using RS1_Ispit_2017.Helper;
using eDostava.Data.Models;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    public class SessionController : AdminController
    {
        private MojContext context;

        public SessionController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
            return View(PrepareAllSession());
        }

        public IActionResult Obrisi(string token)
        {
            AuthToken x = context.AuthTokeni.Where(t => t.Value == token).FirstOrDefault();

            context.AuthTokeni.Remove(x);
            context.SaveChanges();

            return PartialView("Index", PrepareAllSession());
        }

        private SessionPrikazVM PrepareAllSession()
        {
            return new SessionPrikazVM
            {
                Sessions = context.AuthTokeni
                .Select(t => new SessionPrikazVM.Session
                {
                    Ip = t.Ip,
                    LoginTime = t.DatumGenerisanja,
                    Token = t.Value,
                })
                .ToList(),
                TrenutniToken = Autentifikacija.GetCurrentToken(HttpContext)
            };
        }
    }
}