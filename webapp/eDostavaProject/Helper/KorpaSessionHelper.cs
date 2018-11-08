using eDostava.Data.Models;
using Microsoft.AspNetCore.Http;
using RS1_Ispit_2017.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorpaSessionExtensions
{
    // Session Helper za Korpu
    public static class KorpaSessionHelper
    {
        private const string _stavke = "stavke";
        private const string _narudzba = "narudzba";
        private const string _jeNarucilac = "jeNarucilac";

        public static bool? JeNarucilac(this ISession session) => session.GetBoolean(_jeNarucilac); 

        // Prima narudzbu (Narudzba) i setuje u korisnicku sesiju
        public static void SetNarudzba(this HttpContext context, Narudzba narudzba) => context.Session.Set(_narudzba, narudzba);

        // Vraca postojecu ili inicijalizuje i vraca novu narudzbu
        public static Narudzba GetNarudzba(this HttpContext context) => context.Session.Get<Narudzba>(_narudzba) ?? InitNarudzba(context);

        // Inicijalizuje novu narudzbu i setuje u sesiju
        public static Narudzba InitNarudzba(this HttpContext context)
        {
            Narucilac narucilac = context.GetLogiranogNarucioca();
            Narudzba narudzba = new Narudzba{
                NarucilacID = narucilac.KorisnikID
            };

            SetNarudzba(context, narudzba);

            return narudzba;
        }

        public static Narudzba IsporuciNarudzbu(this HttpContext context)
        {
            Narudzba narudzba = new Narudzba {
                Status = Stanje.Isporucena
            };

            SetNarudzba(context, narudzba);

            return narudzba;
        }

        // Prima listu stavki (List<StavkaNarudzbe>) i setuje u korisničku sesiju 
        public static void SetStavke(this HttpContext context, List<StavkaNarudzbe> stavke) => context.Session.Set(_stavke, stavke);

        // Vraća listu stavki (List<StavkaNarudzbe>) iz  sesije
        public static List<StavkaNarudzbe> GetStavke(this HttpContext context) => context.Session.Get<List<StavkaNarudzbe>>(_stavke) ?? new List<StavkaNarudzbe>();

        // Resetuje listu stavki (List<StavkeNarudzbe>) i setuje u sesiju
        public static List<StavkaNarudzbe> InitStavke(this HttpContext context)
        {
            List<StavkaNarudzbe> stavke = new List<StavkaNarudzbe>();
            SetStavke(context, stavke);

            return stavke;
        }
    }

}
