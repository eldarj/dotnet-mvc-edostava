using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace eDostava.Data.Models
{
    public class RestoranLike
    {
        [Key]
        public int RestoranLikeID { get; set; }
        [ForeignKey("Narucilac")]
        public int NarucilacID { get; set; }

        public Narucilac Narucilac { get; set; }
 
        [ForeignKey("Restoran")]
        public int RestoranID { get; set; }
        
        public Restoran Restoran { get; set; }
        public DateTime Datum { get; set; } = DateTime.Now;

        public string Recenzija { get; set; } = "-";


    }
}
