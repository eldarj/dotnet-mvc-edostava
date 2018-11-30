using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models
{
    public class RestoranApiModel
    {
        public class RestoranLike
        {
            public string ImePrezime { get; set; }
            public string Recenzija { get; set; }
            public DateTime Datum { get; set; }
        }
        public class RestoranInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public Vlasnik Vlasnik { get; set; }
            public string Telefon { get; set; }
            public string Lokacija { get; set; }
            public List<RestoranLike> Lajkovi { get; set; }
            public string Slika { get; set; }
            public string Slogan { get; set; }
            public string WebUrl { get; set; }
            public List<TipKuhinje> TipoviKuhinje { get; set; }
        }
        public List<RestoranInfo> Restorani { get; set; }
    }
}
