using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using RS1_Ispit_2017.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.Filters
{
    public class AuthAttribute : TypeFilterAttribute
    {
        public AuthAttribute(bool moderator) : base(typeof(Auth))
        {
            Arguments = new object[] { moderator };
        }
    }

    public class Auth : IAsyncActionFilter
    {
        private readonly bool _moderator;

        public Auth(bool moderator)
        {
            _moderator = moderator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Moderator moderator = Autentifikacija.GetLogiranogModeratora(context.HttpContext);

            if (moderator == null)
            {
                ErrorAndRedirect(context);
                return;
            }

            MojContext db = context.HttpContext.RequestServices.GetService<MojContext>();

            if (_moderator && db.Moderatori.Any(m => m.KorisnikID == moderator.KorisnikID))
            {
                await next();
                return;
            }

            ErrorAndRedirect(context);
        }

        private void ErrorAndRedirect(ActionExecutingContext context)
        {
            if (context.Controller is Controller controller)
            {
                controller.TempData["Error"] = "Nemate pravo pristupa!";
            }

            context.Result = new RedirectToActionResult("Index", "Login", new { area = "" });
        }
    }
}
