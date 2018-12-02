using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models
{
    public class RestoranLikeRequest
    {
        public UserLoginRequest credentials { get; set; }
        public string Recenzija { get; set; }
    }
}
