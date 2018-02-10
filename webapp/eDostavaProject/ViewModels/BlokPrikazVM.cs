using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class BlokPrikazVM
    {
        public class BlokPrikazInfo
        {
            public int BlokId { get; set; }
            public string Naziv { get; set; }
            public int BrojNarucioca { get; set; }
        }
        public int GradId { get; set; }
        public List<BlokPrikazInfo> Blokovi;
    }
}
