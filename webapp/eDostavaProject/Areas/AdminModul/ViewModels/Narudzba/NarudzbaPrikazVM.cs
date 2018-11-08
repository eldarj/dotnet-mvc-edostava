using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels.Narudzba
{
    public class NarudzbaPrikazVM
    {
        public class NarudzbaPrikazInfo
        {
            public int Id { get; set; }
            public string Sifra { get; set; }
            public DateTime DatumVrijeme { get; set; }
            public double UkupnaCijena { get; set; }
            public int UkupnoStavki { get; set; }
            public string NarucilacImePrezime { get; set; }
            public int NarucilacId { get; set; }
        }
        public List<NarudzbaPrikazInfo> Narudzbe;
    }
}
