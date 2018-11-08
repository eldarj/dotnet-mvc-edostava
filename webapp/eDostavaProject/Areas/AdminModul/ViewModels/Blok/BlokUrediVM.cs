using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class BlokUrediVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Naziv { get; set; }

        public int GradID { get; set; }

        public List<SelectListItem> Gradovi { get; set; }

        public bool PredefinedGrad { get; set; }
    }
}
