using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models.RequestModels
{
    public class NarudzbaRequest
    {
        public UserLoginRequest credentials { get; set; }
        public int Id{ get; set; }
    }
}
