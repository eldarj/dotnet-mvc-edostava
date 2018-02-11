using eDostava.Data;
using eDostava.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eDostava.Web.ViewModels;

namespace eDostava.Web.ViewComponents
{
    [ViewComponent(Name ="RestoraniVlasnika")]
    public class RestoraniVlasnikaViewComponent : ViewComponent
    {
        private MojContext context;
        public RestoraniVlasnikaViewComponent(MojContext db)
        {
            context = db;
        }
        public async Task<IViewComponentResult> InvokeAsync (int VlasnikId)
        {
            RestoranIndexVM Model = new RestoranIndexVM();
            Model.vlasnik = context.Vlasnici.Find(VlasnikId);
            Model.Rows = context.Restorani
                .Select(x => new RestoranIndexVM.Row()
                {
                    RestoranID = x.RestoranID,
                    nazivRestorana = x.Naziv,
                    lokacijaRestorana = x.Blok.Grad.Naziv + ", " + x.Blok.Naziv,
                    brojTelefona = x.Telefon
                })
                .ToList();
            return View(Model);
        }
    }
}
