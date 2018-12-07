using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Web.Areas.Api.Helper;

namespace eDostava.Web.Areas.Api.Filters
{
    public class MyApiAuthAttribute : TypeFilterAttribute
    {

        public MyApiAuthAttribute()
            : base(typeof(ApiAuthImpl))
        {
            Arguments = new object[] { };
        }
    }

    public class ApiAuthImpl : IAsyncActionFilter
    {
        public ApiAuthImpl() { }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Narucilac user = context.HttpContext.GetKorisnikOfAuthToken();

            if (user != null)
            {
                await next();
                return;
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
