using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class JelovnikVM
    {

        public string nazivRestorana;
        public string lokacija;
        public int lajkovi;
        public int minimalanIznos;
        public string telefon;
        public string vlasnik;
        public int restoranID { get; set; }
        public class Row
        {
            public int JelovnikID;
            public string OpisJelovnika;
            public bool isAktivan;
        }

        public List<Row> Rows;
    }
}
