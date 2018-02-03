using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class RestoranIndexVM
    {
        public class Row
        {
            public int RestoranID;
            public string lokacijaRestorana;
            public string opis;
            public int minimalnaCijenaNarudzbe;
            public string vlasnik;
            public List<SelectListItem> radnoVrijeme;
            public string brojTelefona;
            public int brojLajkova;
            public string nazivRestorana;

        }

        public List<Row>Rows;


    }
}
