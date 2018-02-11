using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class BlokDodajVM
    {
        public int BlokId { get; set; }
        [Required(ErrorMessage ="Obavezno polje!")]
        public string nazivBloka { get; set; }
        public string nazivGrada { get; set; }
        public int GradId { get; set; }

    }
}
