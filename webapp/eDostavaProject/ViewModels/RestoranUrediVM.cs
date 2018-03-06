using eDostava.Data.Models;
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
    public class RestoranUrediVM
    {
        public string vlasnikNaziv { get; set; }
        public string BlokNaziv { get; set; }
        public int RestoranId { get; set; }
        public string naziv { get; set; }
        [Required(ErrorMessage = "Required!")]
        public string opis { get; set; }
        [RegularExpression(@"^[0][6,3]\d-\d{3}-\d{3}$", ErrorMessage = "Required regex formats are:06X-XXX-XXX / 03X-XXX-XXX")]
        public string brTelefona { get; set; }
        [Required(ErrorMessage = "Required!"), Range(1, 30, ErrorMessage = "Integer <=30 required!")]
        public int minimalnaCijenaNarudz { get; set; }
        public int vlasnikId { get; set; }
        public int blokId { get; set; }
        public List<SelectListItem> blokovi;
        public List<SelectListItem> vlasnici;
        public List<Jelovnik> jelovnici;

    }
}
