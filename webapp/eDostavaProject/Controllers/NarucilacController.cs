using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RS1_Ispit_2017.Helper;
using eDostava.Web.ViewModels;

namespace eDostava.Web.Controllers
{
   
    public class NarucilacController : Controller
    {
        private MojContext context;
        public NarucilacController(MojContext db)
        {
            context = db;
        }

        public IActionResult LikeRestoran(int restoranid)
        {
            if (HttpContext.GetLogiranogNarucioca() != null)
            {
                int narucilacid = HttpContext.GetLogiranogNarucioca().KorisnikID;
                RestoranLike n = new RestoranLike
                {
                    NarucilacID = narucilacid,
                    RestoranID = restoranid,
                };
                context.Lajkovi.Add(n);
                context.SaveChanges();

                return RedirectToAction("Index", "Restorani");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult UnlikeRestoran(int restoranid)
        {
            if (HttpContext.GetLogiranogNarucioca() != null)
            {
                int narucilacid = HttpContext.GetLogiranogNarucioca().KorisnikID;
              
                RestoranLike n = context.Lajkovi.Where(x => x.RestoranID == restoranid && x.NarucilacID == narucilacid).FirstOrDefault();
                context.Remove(n);
                context.SaveChanges();

                return RedirectToAction("Index", "Restorani");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }
}