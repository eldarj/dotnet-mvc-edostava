using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace eDostava.Data.Models
{
    public class Grad
    {
        [Key]
        public int GradID { get; set; }

        public string Naziv { get; set; }

        public int PoštanskiBroj { get; set; }

        public string Drzava { get; set; }
    }
}
