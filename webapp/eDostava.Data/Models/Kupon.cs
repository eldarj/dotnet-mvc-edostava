using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{
   public class Kupon
    {[Key]
        public int KuponID { get; set; }
        public double Vrijednost { get; set; }
        public string Opis { get; set; }

    }
}
