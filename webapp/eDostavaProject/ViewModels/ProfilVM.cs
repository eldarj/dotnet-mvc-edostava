using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class ProfilVM
    {
        public int ProfilID { get; set; }
        public string Ime { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
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
