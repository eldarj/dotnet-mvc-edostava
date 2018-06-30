using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Data;
using eDostava.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using eDostava.Data.Models;

namespace eDostava.Web.Controllers
{
    public class ModeratorController : Controller
    {
        private MojContext context;
        public ModeratorController(MojContext db)
        {
            context = db;
        }

        public IActionResult Index()
        {
           
            return View();
        }
    }

}