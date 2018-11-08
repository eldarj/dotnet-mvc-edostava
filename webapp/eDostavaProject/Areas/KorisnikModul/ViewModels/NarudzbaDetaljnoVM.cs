using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.KorisnikModul.ViewModels
{
    public class NarudzbaDetaljnoVM
    {
        public string Sifra { get; set; }
        public DateTime DatumVrijeme { get; set; }
        public double UkupnaCijena { get; set; }
        public List<StavkaNarudzbe> Stavke { get; set; }
        public List<string> NarucenoIzRestorana { get; set; }
    }
}
