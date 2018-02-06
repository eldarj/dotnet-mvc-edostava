using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace eDostava.Web.ViewModels
{
    public class HranaUrediVM
    {

        public int divID { get; set; }
        public int jelovnikID { get; set; }
        public List<SelectListItem> jelovnici;
        public int HranaID { get; set; }
        [Required(ErrorMessage ="Required!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Required!"),Range(1.0,30.0,ErrorMessage ="Maksimalna vrijednost je 30!")]
        public double cijena { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string opis { get; set; }
        public string prilog { get; set; }
        public int tipkuhinjeID { get; set; }
        [Required(ErrorMessage = "Required!"), Range(1,int.MaxValue)]
        public int sifra { get; set; }
        public List<SelectListItem> tipoviKuhinje;
    }
}
