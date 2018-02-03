using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data;
using eDostava.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

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

            return View(model);
        }
    }
}