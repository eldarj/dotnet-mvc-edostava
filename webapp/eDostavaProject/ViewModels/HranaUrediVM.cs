using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eDostava.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace eDostava.Web.ViewModels
{
    public class HranaUrediVM
    {

        public int divID { get; set; }
        public int jelovnikID { get; set; }
        public List<SelectListItem> jelovnici;
        public int HranaID { get; set; }
        [Required(ErrorMessage ="Required!")]
        [Remote(action: nameof(HranaController.ValidacijaNaziv), controller: "Hrana", AdditionalFields = nameof(jelovnikID))]
        public string Naziv { get; set; }
        [Required(ErrorMessage = "Required!"),Range(1.0,30.0,ErrorMessage ="Maksimalna vrijednost je 30!")]
        public double cijena { get; set; }
        [Required(ErrorMessage = "Required!"), RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters allowed.")]
        public string opis { get; set; }
        [RegularExpression("^[a-zA-Z]+$",ErrorMessage ="Only letters allowed.")]
        public string prilog { get; set; }
        public int tipkuhinjeID { get; set; }
        [Required(ErrorMessage = "Required!"), Range(1,int.MaxValue)]
        [Remote(action:nameof(HranaController.ValidacijaSifra),controller:"Hrana")]
        public int sifra { get; set; }
        public List<SelectListItem> tipoviKuhinje;
    }
}
