using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{
    public class Obavjestenje
    {
        [Key]
        public int ObavjestenjeID { get; set; }

        public string Poruka { get; set; }
    }
}
