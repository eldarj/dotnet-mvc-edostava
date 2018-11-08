using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using eDostava.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using eDostava.Web.Areas.AdminModul.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class RestoranUrediVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Naziv { get; set; }

        [StringLength(200, MinimumLength = 10, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, te biti dužine od 10 do 200 slova.")]
        public string Opis { get; set; }

        [RegularExpression(@"\+?[\d\ ]{9,18}", ErrorMessage = "Polje smije sadržati opcionalno karakter '+' i samo brojeve, najmanje 9 i najviše 18.")]
        public string Telefon { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, te biti dužine od 3 do 100 slova.")]
        public string Slogan { get; set; }

        [RegularExpression(@"(www|http:|https:)+[^\s]+[\w]\.[\w]{1,3}[\/\w]+", ErrorMessage = "Polje mora biti u obliku  (www. ili http:)edostava.ba")]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, te biti dužine od 5 do 20 slova.")]
        public string WebUrl { get; set; }

        public IFormFile Slika { get; set; }

        public string SlikaPath { get; set; }

        public int VlasnikId { get; set; }

        public int BlokId { get; set; }

        public List<SelectListItem> Blokovi { get; set; }

        public List<SelectListItem> Vlasnici { get; set; }
        public bool PredefinedVlasnik { get; set; }

    }
}
