using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class GradUrediVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Naziv { get; set; }

        [Range(1, 1000000, ErrorMessage = "Polje smije sadržati samo brojeve, ne manje od 1 i ne veće od 1 000 000.")]
        public int PostanskiBroj { get; set; }
    }
}
