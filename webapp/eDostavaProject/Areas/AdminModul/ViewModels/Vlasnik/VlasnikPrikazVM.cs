using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class VlasnikPrikazVM
    {
        public class VlasnikInfo
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public DateTime DatumKreiranja { get; set; }
            public List<Restoran> Restorani { get; set; }
            public int UkupnoRestorana { get; set; }
        };

        public List<VlasnikInfo> Vlasnici;
    }
}
