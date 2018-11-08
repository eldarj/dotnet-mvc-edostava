using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class ProfilVM
    {
        public int KorisnikID { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Polje smije sadržati samo tekstualne karaktere, najmanje 3 i najviše 20 slova.")]
        public string Prezime { get; set; }

        [RegularExpression(@"\+?[\d\ ]{9,18}", ErrorMessage = "Polje smije sadržati opcionalno karakter '+' i samo brojeve, najmanje 9 i najviše 18.")]
        public string Telefon { get; set; }

        [Required(ErrorMessage = "Obavezno polje!")]
        [EmailAddress(ErrorMessage = "Polje mora biti u formatu email adrese!")]
        public string Email { get; set; }

        public DateTime DatumKreiranja { get; set; }

        public int? BlokId { get; set; }
        public List<SelectListItem> Blok { get; set; }

        public int? BadgeId { get; set; }
        public List<SelectListItem> Badge { get; set; }

        public string Ime_prezime
        {
            get { return Ime + " " + Prezime; }
        }

        public string Prezime_ime
        {
            get { return Prezime + " " + Ime; }
        }
    }
}
