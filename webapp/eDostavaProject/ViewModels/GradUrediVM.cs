using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data.Models;

namespace eDostava.Web.ViewModels
{
    public class GradUrediVM
    {
        public int GradId { get; set; }
        public string Naziv { get; set; }
        public int PostanskiBroj { get; set; }
        public int BrojBlokova { get; set; }
    }
}
