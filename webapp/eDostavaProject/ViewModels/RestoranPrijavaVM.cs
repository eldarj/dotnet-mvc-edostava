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
    public class RestoranPrijavaVM
    {
        public int vlasnikId { get; set; }
        public int blokId { get; set; }

        public List<SelectListItem> blokovi;
        public List<SelectListItem> vlasnici;
        [Required(ErrorMessage ="Required!")]
        [Remote(action: nameof(RestoraniController.ValidacijaRestoran), controller: "Restorani")]
        public string naziv { get; set; }
        [Required(ErrorMessage ="Required!")]
        public string opis { get; set; }
        [Required(ErrorMessage = "Required!"),RegularExpression(@"^[0][6,3]\d-\d{3}-\d{3}$",ErrorMessage = "Required regex formats are: 06X-XXX-XXX / 03X-XXX-XXX")]
        public string brojTelefona { get; set; }
        [Required(ErrorMessage = "Required!"),Range(1,30,ErrorMessage ="Integer <=30 required!")]
        public int minimalnaCijenaNarudzbe { get; set; }
       

    }
}
