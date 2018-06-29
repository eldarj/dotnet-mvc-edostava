using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class KorpaPrikazVM
    {
        public class KorpaStavkeInfo
        {
            public int ProizvodID { get; set; }
            public string Naziv { get; set; }
            public double Cijena { get; set; }
            public string Opis { get; set; }
            public string Prilog { get; set; }
            public int Kolicina { get; set; }
            public int JelovnikID { get; set; }
            public Jelovnik Jelovnik { get; set; }
        }
        public List<KorpaStavkeInfo> Stavke;
        public Narudzba Narudzba { get; set; }

    }
}
