using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eDostava.Data.Models
{
    public class StavkaNarudzbe
    {
        [Key]
        public int StavkeNarudzbeID { get; set; }
        [ForeignKey("Narudzba")]
        public int NarudzbaID { get; set; }
        public Narudzba Narudzba { get; set; }
        
        [ForeignKey("Hrana")]
        public int HranaID { get; set; }
        public Hrana Hrana { get; set; }
        public int Kolicina { get; set; }

    }
}
