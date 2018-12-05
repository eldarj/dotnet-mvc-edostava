using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Helper
{
    public class MyBaseApiController : Controller
    {
        protected readonly MojContext _context;

        protected MyBaseApiController(MojContext db)
        {
            _context = db;
        }

        protected Narucilac MyAuthUser => HttpContext.GetKorisnikOfAuthToken();
    }
}
