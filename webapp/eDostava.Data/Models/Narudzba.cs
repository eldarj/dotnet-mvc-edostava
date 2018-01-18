using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{

    public enum Stanje
    {
        Prihvacena,
        Odbijena,
        Isporucena
    }
    public class Narudzba
    {
        [Key]
        public int NarudzbaID { get; set; }
           
        public int Sifra { get; set; }

        public DateTime DatumVrijeme { get; set; }
        public int UkupnaCijena { get; set; }

       public Stanje Status { get; set; }
        [ForeignKey("Kupon")]
        public  int?  KuponID { get; set; }

        public Kupon Kupon { get; set; }
    }
}
