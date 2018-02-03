using eDostava.Data.Models;
using Microsoft.AspNetCore.Http;
using eDostava.Data.Models;
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

        public static void SetLogiranogKorisnika(this HttpContext context, Korisnik korisnik,string tip)
        {
            if(tip == "vlasnik")
            context.Session.Set(logiraniVlasnik,korisnik);
            if (tip == "moderator")
                context.Session.Set(logiraniModerator, korisnik);
            if (tip == "narucilac")
                context.Session.Set(logiraniNarucilac, korisnik);
        }

        public static Korisnik GetLogiranogVlasnika(this HttpContext context)
        {
            Korisnik x = context.Session.Get<Korisnik>(logiraniVlasnik);
            
            return x;
        }

        public static Korisnik GetLogiranogModeratora(this HttpContext context)
        {
            Korisnik x = context.Session.Get<Korisnik>(logiraniModerator);
            return x;
        }

        public static Korisnik GetLogiranogNarucioca(this HttpContext context)
        {
            Korisnik x = context.Session.Get<Korisnik>(logiraniNarucilac);
            return x;
        }
    }
}
