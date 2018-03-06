using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
namespace eDostava.Data.Models
{
    public enum Dani
    {
        Pon,
        Uto,
        Sri,
        Cet,
        Pet,
        Sub,
        Ned 
    }
    public class RadnoVrijeme
    {
        [Key]
        public int RadnoVrijemeID { get; set; }

        public Dani Dan { get; set; }

        public TimeSpan VrijemeOtvaranja { get; set; }
        public TimeSpan VrijemeZatvaranja { get; set; }
        [ForeignKey("Restoran")]
        public int RestoranID { get; set; }
        public Restoran Restoran { get; set; }

    }
}
