using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eDostava.Web.ViewModels
{
    public class HranaDodajVM
    {
        public int jelovnikID { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Required!"), Range(1.0, 30.0, ErrorMessage = "Maksimalna vrijednost je 30!")]
        public double cijena { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string opis { get; set; }
        public string prilog { get; set; }
        public int tipkuhinjeID { get; set; }
        [Required(ErrorMessage = "Required!"), Range(1, int.MaxValue)]
        public int sifra { get; set; }
        public List<SelectListItem> tipoviKuhinje;
        public int restoranID { get; set; }
    }
}
