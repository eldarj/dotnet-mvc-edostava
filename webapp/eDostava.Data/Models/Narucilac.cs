using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace eDostava.Data.Models
{
    public class Narucilac : Korisnik
    {


        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        [ForeignKey("Blok")]
        public int BlokID { get; set; }

        public virtual Blok Blok { get; set; }
        [ForeignKey("Badge")]
        public int BadgeID { get; set; }
        public Badge Badge { get; set; }

        public string Ime_prezime
        {
            get { return Ime + " " + Prezime; }
        }

        public string Prezime_ime
        {
            get { return Prezime + " " + Ime; }
        }

        public string ImageUrl { get; set; }

        public string Adresa { get; set; }

        public virtual ICollection<Narudzba> Narudzbe {get; set;}

    }
}
