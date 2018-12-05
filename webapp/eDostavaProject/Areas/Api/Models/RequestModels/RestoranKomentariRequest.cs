using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eDostava.Web.Areas.Api.Models.RestoranListResponse;

namespace eDostava.Web.Areas.Api.Models.RequestModels
{
    public class RestoranKomentariRequest
    {
        public UserLoginRequest credentials { get; set; }
        public string ClientID { get; set; }
        public string ClientIP { get; set; }
        public string KomentariHashCode { get; set; }

        public class RestoranKomentariResponse
        {
            public string KomentariHashCode { get; set; }
            public List<RestoranRecenzija> Recenzije { get; set; }
        }
    }
}
