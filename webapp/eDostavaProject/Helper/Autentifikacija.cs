using eDostava.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using eDostava.Data;
using eDostava.Web.Helper;
using Microsoft.EntityFrameworkCore;

namespace RS1_Ispit_2017.Helper
{
    public static class Autentifikacija
    {
        

        private const string logiraniVlasnik = "logiraniVlasnik";
        private const string logiraniModerator = "logiraniModerator";
        private const string logiraniNarucilac = "logiraniNarucilac";


        public static void SetBoolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }
        public static bool? GetBoolean(this ISession session, string key)
        {
            var data = session.Get(key);
            if (data == null)
            {
                return null;
            }
            return BitConverter.ToBoolean(data, 0);
        }

        public static void SetLogiranogVlasnika(this HttpContext context, Vlasnik korisnik, bool cookielogin = false)
        {
           
            context.Session.Set(logiraniVlasnik,korisnik);
            if (cookielogin)
                context.Response.SetCookieJson(logiraniVlasnik, korisnik);
            else
                context.Response.SetCookieJson(logiraniVlasnik, null);
        }

        public static void SetLogiranogModeratora(this HttpContext context, MojContext db, Moderator korisnik, bool cookielogin = false)
        {

            context.Session.Set(logiraniModerator, korisnik);
            if (cookielogin)
            {
                string token = Guid.NewGuid().ToString();
                db.AuthTokeni.Add(new AuthToken
                {
                    Value = token,
                    KorisnikId = korisnik.KorisnikID,
                    DatumGenerisanja = DateTime.Now
                });
                context.Response.SetCookieJson(logiraniModerator, token);
            }
            else
            {
                context.Response.SetCookieJson(logiraniModerator, null);
            }
        }

        public static void SetLogiranogNarucioca(this HttpContext context, Narucilac korisnik, bool cookielogin = false)
        {

            context.Session.Set(logiraniNarucilac, korisnik);
            if (cookielogin)
                context.Response.SetCookieJson(logiraniNarucilac, korisnik);
            else
                context.Response.SetCookieJson(logiraniNarucilac, null);
        }

        public static Vlasnik GetLogiranogVlasnika(this HttpContext context)
        {
            Vlasnik x = context.Session.Get<Vlasnik>(logiraniVlasnik);
            if (x == null)
            {
                x = context.Request.GetCookiesJson<Vlasnik>(logiraniVlasnik);
                context.Session.Set(logiraniVlasnik, x);
            }
            return x;
        }

        public static Moderator GetLogiranogModeratora(this HttpContext context, MojContext db)
        {
            Moderator x = context.Session.Get<Moderator>(logiraniModerator);
            if (x == null)
            {
                string token = context.Request.GetCookiesJson<string>(logiraniModerator);
                if (token == null)
                {
                    return null;
                }

                AuthToken authToken = db.AuthTokeni.SingleOrDefault(k => k.Value == token);

                if (authToken != null)
                {
                    context.Session.Set(logiraniModerator, db.Moderatori.SingleOrDefault(k => k.KorisnikID == authToken.KorisnikId));
                }
            }
            return x;
        }

        public static Narucilac GetLogiranogNarucioca(this HttpContext context)
        {
            Narucilac x = context.Session.Get<Narucilac>(logiraniNarucilac);
            if (x == null)
            {
                x = context.Request.GetCookiesJson<Narucilac>(logiraniNarucilac);
                context.Session.Set(logiraniNarucilac, x);
            }
            return x;
        }
    }
}
