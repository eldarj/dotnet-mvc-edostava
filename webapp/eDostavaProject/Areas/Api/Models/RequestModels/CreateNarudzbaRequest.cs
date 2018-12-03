using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models.RequestModels
{
    public class CreateNarudzbaRequest
    {
        public UserLoginRequest credentials { get; set; }

        public class StavkaRequest
        {
            public int HranaID { get; set; }
            public int Kolicina { get; set; }
        }
        public double UkupnaCijena { get; set; }
        public List<StavkaRequest> stavke { get; set; }

    }
}
