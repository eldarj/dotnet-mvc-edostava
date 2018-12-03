using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eDostava.Web.Areas.Api.Models.HranaListResponse;
using static eDostava.Web.Areas.Api.Models.RestoranListResponse;

namespace eDostava.Web.Areas.Api.Models
{
    public class NarudzbaListResponse
    {
        public class NarudzbaHranaStavka
        {
            public string Naziv { get; set; }
            public double Cijena { get; set; }
            public int Kolicina { get; set; }
        }
        public class NarudzbaInfo
        {
            public int Id { get; set; }
            public Guid GuidSifra { get; set; }
            public DateTime DatumKreiranja { get; set; }
            public double UkupnaCijena { get; set; }
            public string Status { get; set; }
            public List<NarudzbaHranaStavka> HranaStavke { get; set; }
            public List<string> NarucenoIzRestorana { get; set; }
        }

        public List<NarudzbaInfo> Narudzbe { get; set; }
    }
}
