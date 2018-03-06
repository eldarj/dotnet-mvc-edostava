using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eDostava.Web.ViewModels
{
    public class BlokListaVM
    {
        public int blokid { get; set; }
        public List<SelectListItem> blokovi;
    }
}
