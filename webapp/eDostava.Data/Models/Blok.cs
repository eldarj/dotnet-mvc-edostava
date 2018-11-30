using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{
   public class Blok
    {
        [Key]
        public int BlokID { get; set; }

        public string Naziv { get; set; }
        [ForeignKey("Grad")]
        public int GradID { get; set; }

        public virtual Grad Grad { get; set; }
    }
}
