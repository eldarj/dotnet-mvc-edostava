using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class HranaIndexVM
    {
        public int jelovnikID;
        public class Row
        {
            public int HranaID;
            public string Naziv;
            public double cijena;
            public string opis;
            public string prilog;
            public int tipkuhinjeID;
            
        }

        public List<Row> Rows;
    }
}
