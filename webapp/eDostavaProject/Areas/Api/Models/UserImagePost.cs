﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.Areas.Api.Models
{
    public class UserImagePost
    {
        public string EncodedImageBase64 { get; set; }
        public string FileName { get; set; }
        public int UserId { get; set; }
    }
}
