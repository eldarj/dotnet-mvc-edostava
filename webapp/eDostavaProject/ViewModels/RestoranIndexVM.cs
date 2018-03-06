using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class RestoranIndexVM
    {
        public enum Logiran
        {
            Moderator,Vlasnik,Narucilac
        }
        public class Row
        {
            public int RestoranID { get; set; }
            public string lokacijaRestorana;
            public string opis;
            public int minimalnaCijenaNarudzbe;
            public string vlasnik;
            public List<SelectListItem> radnoVrijeme;
            public int radnoVrijemeid { get; set; }
            public string brojTelefona;
            public int brojLajkova;
            public string nazivRestorana;
            public bool jeVlasnikRestorana;
            public bool jeLajkan;
            public bool pretrazen;

        }
        public Vlasnik vlasnik;
        public Logiran jeLogiran;
        public List<Row>Rows;


    }
}
