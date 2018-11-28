using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eDostava.Data.Models
{
    public class AuthToken
    {
        public int Id { get; set; }
        public string Value { get; set; }
        [ForeignKey(nameof(Korisnik))]
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public DateTime DatumGenerisanja { get; set; }

    }
}
