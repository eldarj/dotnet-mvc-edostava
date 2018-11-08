using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class RestoranPrikazVM
    {
        public class RestoranInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public Vlasnik Vlasnik { get; set; }
            public string BrojTelefona { get; set; }
            public string Lokacija { get; set; }
            public int BrojLajkova { get; set; }
            public string Slika { get; set; }
            public string Slogan { get; set; }
            public string WebUrl { get; set; }
        }
        public List<RestoranInfo> Restorani { get; set; }
    }
}
