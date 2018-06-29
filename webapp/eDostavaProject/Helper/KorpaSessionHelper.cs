using eDostava.Data.Models;
using Microsoft.AspNetCore.Http;
using RS1_Ispit_2017.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KorpaSessionExtensions
{
    public static class KorpaSessionHelper
    {
        private const string _stavke = "stavke";
        private const string _narudzba = "narudzba";
        private const string _jeNarucilac = "jeNarucilac";

        public static bool? JeNarucilac(this ISession session) => session.GetBoolean(_jeNarucilac); 

        /// <summary>
        /// Prima listu <see cref="List{T}"/> sa stavkama - <see cref="StavkaNarudzbe"/> i setuje u korisničku sesiju.
        /// </summary>
        /// <param name="stavke"><see cref="List{T}"/> od <see cref="StavkaNarudzbe"/></param>
        public static void SetNarudzba(this HttpContext context, Narudzba narudzba) => context.Session.Set(_narudzba, narudzba);

        /// <summary>
        /// Prima listu <see cref="List{T}"/> sa stavkama - <see cref="StavkaNarudzbe"/> i setuje u korisničku sesiju.
        /// </summary>
        /// <param name="stavke"><see cref="List{T}"/> od <see cref="StavkaNarudzbe"/></param>
        public static Narudzba GetNarudzba(this HttpContext context) => context.Session.Get<Narudzba>(_narudzba) ?? InitNarudzba(context);

        public static Narudzba InitNarudzba(this HttpContext context)
        {
            Narudzba narudzba = new Narudzba();

            SetNarudzba(context, narudzba);

            return narudzba;
        }
        /// <summary>
        /// Prima listu <see cref="List{T}"/> sa stavkama - <see cref="StavkaNarudzbe"/> i setuje u korisničku sesiju.
        /// </summary>
        /// <param name="stavke"><see cref="List{T}"/> od <see cref="StavkaNarudzbe"/></param>
        public static void SetStavke(this HttpContext context, List<StavkaNarudzbe> stavke) => context.Session.Set(_stavke, stavke);

        /// <summary>
        /// Vraća listu <see cref="List{T}"/> sa stavkama - <see cref="StavkaNarudzbe"/> iz korisničke sesije.
        /// </summary>
        public static List<StavkaNarudzbe> GetStavke(this HttpContext context) => context.Session.Get<List<StavkaNarudzbe>>(_stavke) ?? new List<StavkaNarudzbe>();
    }

}
