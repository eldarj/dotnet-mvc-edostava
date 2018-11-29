using eDostava.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.AdminModul.ViewModels
{
    public class SessionPrikazVM
    {

        public class Session
        {
            public string Ip { get; set; }
            public string Token { get; set; }
            public string Username { get; set; }
            public DateTime LoginTime { get; set; }
        }

        public List<Session> Sessions { get; set; }
        public string TrenutniToken { get; set; }

    }
}
