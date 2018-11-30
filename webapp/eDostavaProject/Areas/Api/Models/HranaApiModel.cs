﻿using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models
{
    public class HranaApiModel
    {
        public class HranaInfo
        {
            public int Id { get; set; }
            public string Slika { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public double Cijena { get; set; }
            public TipKuhinje Tip { get; set; }
        }
        public List<HranaInfo> Hrana { get; set; }
    }
}
