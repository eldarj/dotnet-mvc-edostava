using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class ProfilLozinkaVM
    {
        public int KorisnikID { get; set; }
        public string Password { get; set; }
        public string NoviPassword { get; set; }
        public string NoviPasswordPonovo { get; set; }
    }
}
