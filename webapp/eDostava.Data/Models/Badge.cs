using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{
    public class Badge
    {
        [Key]
        public  int BadgeID { get; set; }
        public string Naziv { get; set; }
        public int BrojBodova { get; set; }
    }
}
