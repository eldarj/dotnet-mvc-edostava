using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eDostava.Data.Models
{
   public class Jelovnik
    {
        [Key]
        public int JelovnikID { get; set; }

        public string Slika { get; set; }

        public bool Aktivan { get; set; }
        public string Opis { get; set; }
        [ForeignKey("Restoran")]
        public int RestoranID { get; set; }

        public Restoran Restoran { get; set; }
    }
}
