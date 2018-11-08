using eDostava.Data;
using eDostava.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using KorpaSessionExtensions;

namespace eDostava.Web.ViewComponents
{
    [ViewComponent(Name = "Korpa")]
    public class KorpaViewComponent : ViewComponent
    {
        private MojContext context;
        public KorpaViewComponent(MojContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            return View(new KorpaPrikazVM()
            {
                Stavke = HttpContext.GetStavke()
                    .Select(x => new KorpaPrikazVM.KorpaStavkeInfo()
                    {
                        ProizvodID = x.HranaID,
                        Naziv = x.Hrana.Naziv,
                        Cijena = x.CalcCijena,
                        Opis = x.Hrana.Opis,
                        Prilog = x.Hrana.Prilog,
                        Kolicina = x.Kolicina,
                        JelovnikID = x.Hrana.JelovnikID,
                        Jelovnik = x.Hrana.Jelovnik,
                    })
                    .ToList(),
                Narudzba = HttpContext.GetNarudzba()
            });
        }
    }
}
