using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eDostava.Data.Models
{
    public class RestoranRecenzija
    {
        [Key]
        public int RestoranRecenzijaID { get; set; }
        [ForeignKey("Narucilac")]
        public int NarucilacID { get; set; }

        public Narucilac Narucilac { get; set; }

        [ForeignKey("Restoran")]
        public int RestoranID { get; set; }

        public Restoran Restoran { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;
        public String Recenzija { get; set; }
    }
}
