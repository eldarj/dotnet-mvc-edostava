using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data.Models;

namespace eDostava.Web.ViewModels
{
    public class GradPrikazVM
    {
        public class Row
        {
            public int GradId { get; set; }
            public string Naziv { get; set; }
            public int PostanskiBroj { get; set; }
            public int UkupnoNarucioca { get; set; }
        }
        public List<Row> Rows;

    }
}
