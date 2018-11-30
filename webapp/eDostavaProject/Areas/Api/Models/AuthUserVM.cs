using eDostava.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models
{
    public class AuthUserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Adresa { get; set; }
        public string ImageUrl { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public Blok Blok { get; set; }
    }
}
