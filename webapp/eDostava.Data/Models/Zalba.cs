using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{
    public class Zalba
    {
        [Key]
        public  int ZalbaID { get; set; }
        public string Razlog { get; set; }
        public string Obrazlozenje { get; set; }
    }
}
