using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace eDostava.Data.Models
{
    public class Restoran
    {
        [Key]
        public int RestoranID { get; set; }
        public string Naziv { get; set; }
        public string Slika { get; set; }

        public string Slogan { get; set; }

        public string Opis { get; set; }
        public string WebUrl { get; set; }
        public string Telefon { get; set; }
        public int MinimalnaCijenaNarudžbe { get; set; }
        [ForeignKey("Vlasnik")]
        public int VlasnikID { get; set; }

        /* Vlasnik restorana */
        public Vlasnik Vlasnik { get; set; }

        public string Email { get; set; }

        public string Adresa { get; set; }

        [ForeignKey("Blok")]
        public int BlokID { get; set; }

        public Blok Blok { get; set; }

        public virtual ICollection<RestoranLike> Lajkovi { get; set; }

    }
}
