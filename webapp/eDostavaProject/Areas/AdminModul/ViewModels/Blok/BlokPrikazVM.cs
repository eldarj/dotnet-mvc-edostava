using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class BlokPrikazVM
    {
        public class BlokPrikazInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
            public Grad Grad { get; set; }
            public int BrojNarucioca { get; set; }
            public List<Narucilac> Narucioci { get; set; }
        }
        public List<BlokPrikazInfo> Blokovi;
    }
}
