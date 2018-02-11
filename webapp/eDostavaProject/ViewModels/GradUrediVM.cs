using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace eDostava.Web.ViewModels
{
    public class GradUrediVM
    {
        public int GradId { get; set; }
        [Required(ErrorMessage = "Obavezno polje!"), StringLength(30,ErrorMessage = "Naziv ne smije biti duži od 30 karaktera!")]
        public string Naziv { get; set; }
        public int PostanskiBroj { get; set; }
        public int BrojBlokova { get; set; }
    }
}
