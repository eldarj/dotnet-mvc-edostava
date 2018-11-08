using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data.Models;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class GradPrikazVM
    {
        public class GradInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public int PostanskiBroj { get; set; }
            public int UkupnoNarucioca { get; set; }
            public List<Narucilac> Narucioci { get; set; }
            public int UkupnoBlokova { get; set; }
            public List<Blok> Blokovi { get; set; }
        }
        public List<GradInfo> Gradovi;

    }
}
