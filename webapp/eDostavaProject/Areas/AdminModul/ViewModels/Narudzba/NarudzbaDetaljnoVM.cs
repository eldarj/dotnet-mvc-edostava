using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels.Narudzba
{
    public class NarudzbaDetaljnoVM
    {
        public List<StavkaNarudzbe> Stavke { get; set; }
        public List<string> NarucenoIzRestorana { get; set; }
    }
}
