using eDostava.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        public const string tokenCookieName = "EDOSTAVA_TKN";

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
            {
                MojContext db = context.RequestServices.GetService<MojContext>();

                string token = Guid.NewGuid().ToString();
                db.AuthTokeni.Add(new AuthToken
                {
                    Value = token,
                    VlasnikId = korisnik.KorisnikID,
                    DatumGenerisanja = DateTime.Now
                });
                db.SaveChanges();
                context.Response.SetCookieToken(tokenCookieName, token);
            }
            else
            {
                context.Response.SetCookieToken(tokenCookieName, null);
            }
        }

        public static void SetLogiranogModeratora(this HttpContext context, Moderator korisnik, bool cookielogin = false)
        {
            context.Session.Set(logiraniModerator, korisnik);

            if (cookielogin)
            {
                MojContext db = context.RequestServices.GetService<MojContext>();

                string token = Guid.NewGuid().ToString();
                db.AuthTokeni.Add(new AuthToken
                {
                    Value = token,
                    ModeratorId = korisnik.KorisnikID,
                    DatumGenerisanja = DateTime.Now
                });
                db.SaveChanges();
                context.Response.SetCookieToken(tokenCookieName, token);
            }
            else
            {
                context.Response.SetCookieToken(tokenCookieName, null);
            }
        }

        public static void SetLogiranogNarucioca(this HttpContext context, Narucilac korisnik, bool cookielogin = false)
        {

            context.Session.Set(logiraniNarucilac, korisnik);

            if (cookielogin)
            {
                MojContext db = context.RequestServices.GetService<MojContext>();

                string token = Guid.NewGuid().ToString();
                db.AuthTokeni.Add(new AuthToken
                {
                    Value = token,
                    NarucilacId = korisnik.KorisnikID,
                    DatumGenerisanja = DateTime.Now
                });
                db.SaveChanges();
                context.Response.SetCookieToken(tokenCookieName, token);
            }
            else
            {
                context.Response.SetCookieToken(tokenCookieName, null);
            }
        }

        public static Vlasnik GetLogiranogVlasnika(this HttpContext context)
        {
            Vlasnik x = context.Session.Get<Vlasnik>(logiraniVlasnik);
            if (x == null)
            {
                string token = context.Request.GetCookiesToken(tokenCookieName);
                if (token == null)
                {
                    return null;
                }

                MojContext db = context.RequestServices.GetService<MojContext>();

                Vlasnik korisnik = db.AuthTokeni
                    .Where(k => k.Value == token)
                    .Select(k => k.Vlasnik)
                    .SingleOrDefault();

                if (korisnik != null)
                {
                    context.Session.Set(logiraniVlasnik, korisnik);
                    context.Session.Set("trenutnoLogiran", korisnik.Ime_prezime);
                    return korisnik;
                }
            }
            return x;
        }

        public static Moderator GetLogiranogModeratora(this HttpContext context)
        {
            Moderator x = context.Session.Get<Moderator>(logiraniModerator);
            if (x == null)
            {
                string token = context.Request.GetCookiesToken(tokenCookieName);
                if (token == null)
                {
                    return null;
                }

                MojContext db = context.RequestServices.GetService<MojContext>();

                Moderator korisnik = db.AuthTokeni
                    .Where(k => k.Value == token)
                    .Select(k => k.Moderator)
                    .SingleOrDefault();

                if (korisnik != null)
                {
                    context.Session.Set(logiraniModerator, korisnik);
                    context.Session.Set("trenutnoLogiran", korisnik.Username);
                    return korisnik;
                }
            }
            return x;
        }

        public static Narucilac GetLogiranogNarucioca(this HttpContext context)
        {
            Narucilac x = context.Session.Get<Narucilac>(logiraniNarucilac);
            if (x == null)
            {
                string token = context.Request.GetCookiesToken(tokenCookieName);
                if (token == null)
                {
                    return null;
                }

                MojContext db = context.RequestServices.GetService<MojContext>();

                Narucilac korisnik = db.AuthTokeni
                    .Where(k => k.Value == token)
                    .Select(k => k.Narucilac)
                    .SingleOrDefault();

                if (korisnik != null)
                {
                    context.Session.Set(logiraniNarucilac, korisnik);
                    context.Session.Set("trenutnoLogiran", korisnik.Ime_prezime);
                    context.Session.SetBoolean("jeNarucilac", true);
                    return korisnik;
                }
            }
            return x;
        }

        public static string GetCurrentToken(this HttpContext context)
        {
            return context.Request.GetCookiesToken(tokenCookieName);
        }

        public static void RemoveCurrentSession(this HttpContext context)
        {
            string token = context.Request.GetCookiesToken(tokenCookieName);

            MojContext db = context.RequestServices.GetService<MojContext>();
            AuthToken dbToken = db.AuthTokeni.SingleOrDefault(t => t.Value == token);
            if (dbToken != null)
            {
                db.AuthTokeni.Remove(dbToken);
                db.SaveChanges();
            }

            context.Response.Cookies.Delete(tokenCookieName);
            context.Session.Clear();
        }
    }
}
