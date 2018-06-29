using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class NarucilacPrikazVM
    {
        public class NaruciociInfo
        {
            public Narucilac Narucilac;
            public int NarucilacId { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string DatumKreiranja { get; set; }
            public string Telefon { get; set; }
            public string BadgeNaziv { get; set; }
            public string BlokNaziv { get; set; }
            public string GradNaziv { get; set; } //izbrisati sve reference, jer sad imamo Grad objekat
            public Grad Grad { get; set; }
            public int PostanskiBroj { get; set; }
        }
        public Grad Grad { get; set; }
        public List<NaruciociInfo> Narucioci;
    }
}
