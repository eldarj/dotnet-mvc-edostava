using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class NarucilacUrediVM
    {
        public int NarucilacID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DatumKreiranja { get; set; }
        public int UkupnoNarudzbi { get; set; }
        public int BlokID { get; set; }
        public int BadgeID { get; set; }
        public List<SelectListItem> Blokovi { get; set; }
        public List<SelectListItem> Badgevi { get; set; }
    }
}
