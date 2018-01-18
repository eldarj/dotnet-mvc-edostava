using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace eDostava.Data.Models
{
  
   public class TipKuhinje
    {
        [Key]
        public int TipKuhinjeID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
