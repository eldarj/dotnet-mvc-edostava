using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class NarucilacDetaljnoVM
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string DatumKreiranja { get; set; }
        public string Telefon { get; set; }
        public string BlokNaziv { get; set; }
        public string GradNaziv { get; set; }
        public int PostanskiBroj { get; set; }
        public int UkupnoNarudzbi { get; set; }
        public double UkupnoPotroseno { get; set; }
    }
}
