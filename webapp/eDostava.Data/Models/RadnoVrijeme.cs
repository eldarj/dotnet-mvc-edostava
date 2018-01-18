using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
namespace eDostava.Data.Models
{
    public enum Dani
    {
        Ponedjeljak,
        Utorak,
        Srijeda,
        Cetvrtak,
        Petak,
        Subota,
        Nedjelja 
    }
    public class RadnoVrijeme
    {
        [Key]
        public int RadnoVrijemeID { get; set; }

        public Dani Dan { get; set; }

        public TimeSpan VrijemeOtvaranja { get; set; }
        public TimeSpan VrijemeZatvaranja { get; set; }

    }
}
