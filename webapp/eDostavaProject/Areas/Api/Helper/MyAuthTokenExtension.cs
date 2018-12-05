using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Helper
{
    public static class MyAuthTokenExtension
    {
        public static Narucilac GetKorisnikOfAuthToken(this HttpContext context)
        {
            MojContext db = context.RequestServices.GetService<MojContext>();

            string token = context.GetMyAuthToken();
            Narucilac user = db.AuthTokeni.Where(x => token != null && x.Value == token).Select(s => s.Narucilac).SingleOrDefault();
            return user;
        }

        public static string GetMyAuthToken(this HttpContext context)
        {
            string token = context.Request.Headers["MyAuthToken"];
            return token;
        }
    }
}
