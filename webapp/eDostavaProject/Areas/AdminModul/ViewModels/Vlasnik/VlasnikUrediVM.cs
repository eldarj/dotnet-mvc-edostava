using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class VlasnikUrediVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Ime smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Prezime smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Username smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Email smije sadržati samo tekstualne karaktere, te biti dužine od 6 do 50 slova.")]
        public string Email { get; set; }
    }
}
