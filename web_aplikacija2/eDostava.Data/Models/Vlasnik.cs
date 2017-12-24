using System;
using System.Collections.Generic;
using System.Text;

namespace eDostava.Data.Models
{
   public class Vlasnik:Korisnik
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Ime_prezime
        {
            get { return Ime + " " + Prezime; }
        }

        public string Prezime_ime
        {
            get { return Prezime + " " + Ime; }
        }



    }
}
