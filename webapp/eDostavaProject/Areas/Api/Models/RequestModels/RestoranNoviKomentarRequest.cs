using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models.RequestModels
{
    public class RestoranNoviKomentarRequest
    {
        public AuthLoginVM credentials { get; set; }
        public string komentar { get; set; }
    }
}
