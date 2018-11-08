using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Web.ViewModels;
using eDostava.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eDostava.Web.Areas.AdminModul.Controllers
{
    [Area("AdminModul")]
    public class TestController : Controller
    {
        private MojContext context;
        public TestController(MojContext _db)
        {
            context = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PrijaviRestoran()
        {
            RestoranPrijavaVM model = new RestoranPrijavaVM();


            model.blokovi = context.Blokovi.Include(x => x.Grad).Select(x => new SelectListItem
            {
                Text = x.Naziv + ", " + x.Grad.Naziv,
                Value = x.BlokID.ToString()

            }).ToList();

            model.vlasnici = context.Vlasnici.Select(x => new SelectListItem
            {
                Text = x.Ime_prezime,
                Value = x.KorisnikID.ToString()

            }).ToList();

            return PartialView(model);
        }
    }
}