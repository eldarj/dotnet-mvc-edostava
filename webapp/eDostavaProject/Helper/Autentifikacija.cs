using eDostava.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using eDostava.Data;

namespace RS1_Ispit_2017.Helper
{
    public static class Autentifikacija
    {
        

        private const string logiraniVlasnik = "logiraniVlasnik";
        private const string logiraniModerator = "logiraniModerator";
        private const string logiraniNarucilac = "logiraniNarucilac";

        public static void SetLogiranogVlasnika(this HttpContext context, Vlasnik korisnik)
        {
           
            context.Session.Set(logiraniVlasnik,korisnik);
            
        }

        public static void SetLogiranogModeratora(this HttpContext context, Moderator korisnik)
        {

            context.Session.Set(logiraniModerator, korisnik);

        }

        public static void SetLogiranogNarucioca(this HttpContext context, Narucilac korisnik)
        {

            context.Session.Set(logiraniNarucilac, korisnik);

        }

        public static Vlasnik GetLogiranogVlasnika(this HttpContext context)
        {
            Vlasnik x = context.Session.Get<Vlasnik>(logiraniVlasnik);
            
            return x;
        }

        public static Moderator GetLogiranogModeratora(this HttpContext context)
        {
            Moderator x = context.Session.Get<Moderator>(logiraniModerator);
            return x;
        }

        public static Narucilac GetLogiranogNarucioca(this HttpContext context)
        {
            Narucilac x = context.Session.Get<Narucilac>(logiraniNarucilac);
            return x;
        }
    }
}
