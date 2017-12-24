using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace eDostava.Data.Models
{
   public abstract class Korisnik
    {
        [Key]
        public int KorisnikID { get; set; }
        public string Username { get; set; }
        public string Password  { get; set; }
        public string Email { get; set; }

        public DateTime DatumKreiranja { get; set; }
    }
}
