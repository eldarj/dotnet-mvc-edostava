using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.KorisnikModul.ViewModels
{
    public class NarudzbaPrikazVM
    {
        public class NarudzbaPrikazInfo
        {
            public int NarudzbaId { get; set; }
            public string Sifra { get; set; }
            public DateTime DatumVrijeme { get; set; }
            public double UkupnaCijena { get; set; }
        }
        public List<NarudzbaPrikazInfo> Narudzbe;
    }
}
