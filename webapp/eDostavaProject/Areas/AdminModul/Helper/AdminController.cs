using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Web.Areas.AdminModul.Filters;

namespace eDostava.Web.Areas.AdminModul.Helper
{
    [Area("AdminModul")]
    [Auth(moderator: true)]
    public class AdminController : Controller
    {

    }
}