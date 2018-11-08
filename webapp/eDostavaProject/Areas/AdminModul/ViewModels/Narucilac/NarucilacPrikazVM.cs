using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class NarucilacPrikazVM
    {
        public class NaruciociInfo
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public DateTime DatumKreiranja { get; set; }
            public string Telefon { get; set; }
            public string BlokNaziv { get; set; }
            public Grad Grad { get; set; }
            public int PostanskiBroj { get; set; }
            public int UkupnoNarudzbi { get; set; }
        }
        public List<NaruciociInfo> Narucioci;
    }
}
