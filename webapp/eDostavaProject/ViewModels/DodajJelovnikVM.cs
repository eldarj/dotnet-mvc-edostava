using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using eDostava.Web.Controllers;

namespace eDostava.Web.ViewModels
{
    public class DodajJelovnikVM
    {
        public bool aktivan { get; set; }
        [Required(ErrorMessage ="Required!"),RegularExpression("^[a-zA-Z]+$",ErrorMessage ="Only letters allowed")]
        [Remote(action:nameof(RestoraniController.ValidacijaJelovnik),controller:"Restorani",AdditionalFields = nameof(restoranid))]
        public string opis { get; set; }
        public int restoranid { get; set; }
    }
}
