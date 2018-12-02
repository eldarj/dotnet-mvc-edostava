using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eDostava.Web.Areas.AdminModul.ViewModels.RestoranPrikazVM;

namespace eDostava.Web.Areas.Api.Models
{
    public class HranaListResponse
    {
        public class HranaInfo
        {
            public int Id { get; set; }
            public string ImageUrl { get; set; }
            public string Naziv { get; set; }
            public string Opis { get; set; }
            public double Cijena { get; set; }
            public string RestoranNaziv { get; set; }
            public int RestoranId { get; set; }
            public TipKuhinje TipKuhinje { get; set; }
        }
        public List<HranaInfo> Hrana { get; set; }
    }
}
