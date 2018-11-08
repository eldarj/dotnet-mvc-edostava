using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using eDostava.Data.Models;
using eDostava.Web.Areas.AdminModul.ViewModels;

namespace eDostava.Web.Controllers
{
    [Area("AdminModul")]
    public class ModeratorController : Controller
    {
        private MojContext context;
        public ModeratorController(MojContext db)
        {
            context = db;
        }

        //[Route("dashboard/index")]
        public IActionResult Index(string layout)
        {
            ModeratorStatistikaVM Model= new ModeratorStatistikaVM
            {
                UkupnoNarucioca = context.Narucioci.Count(),
                UkupnoRestorana = context.Restorani.Count(),
                UkupnoVlasnika = context.Vlasnici.Count(),
                UkupnoNarudzbi = context.Narudzbe.Count()
            };

            return View(Model);
        }
    }

}