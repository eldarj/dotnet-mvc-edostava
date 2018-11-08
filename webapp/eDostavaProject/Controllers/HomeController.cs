using Microsoft.AspNetCore.Mvc;
using RS1_Ispit_2017.Helper;

namespace eDostavaProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.GetLogiranogNarucioca() == null 
                && HttpContext.GetLogiranogVlasnika() == null 
                && HttpContext.GetLogiranogModeratora() == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.IsHome = true;
            return View("Index");
        }

    }
}
