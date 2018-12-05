using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Helper
{
    public static class MyApiConfig
    {
        public static readonly string IMAGE_DIR = "uploads/images/korisnik";

        public static readonly string DEFAULT_IMAGE = "default_identicon.png";

        public const string AUTH_USER = "logiraniNarucilac";

        public const string TOKEN_COOKIE_NAME = "EDOSTAVA_TKN";
    }
}
