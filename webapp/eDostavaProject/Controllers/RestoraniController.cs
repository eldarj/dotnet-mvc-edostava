using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eDostava.Web.ViewModels;
using eDostava.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using eDostava.Data.Models;
using RS1_Ispit_2017.Helper;

namespace eDostava.Web.Controllers
{
    public class RestoraniController : Controller
    {
        private MojContext context;
        public RestoraniController(MojContext db)
        {
            context = db;
        }

        
        public IActionResult Jelovnik(int restoranid)
        {

            
            
            JelovnikVM model = new JelovnikVM();
            model.Rows = context.Jelovnici.Where(x => x.RestoranID == restoranid).Select(x => new JelovnikVM.Row
            {
                JelovnikID= x.JelovnikID,
                isAktivan=x.Aktivan,
                OpisJelovnika=x.Opis
            }).ToList();

            model.minimalanIznos = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.MinimalnaCijenaNarudžbe).FirstOrDefault();
            model.nazivRestorana = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.Naziv).FirstOrDefault();
            model.lokacija = context.Restorani.Include(x=>x.Blok).Include(x=>x.Blok.Grad).Where(x => x.RestoranID == restoranid).Select(x => x.Blok.Naziv + ", " + x.Blok.Grad.Naziv).FirstOrDefault();
            model.lajkovi = context.Lajkovi.Where(x => x.RestoranID == restoranid).Count();
            model.vlasnik = context.Restorani.Include(x=>x.Vlasnik).Where(x => x.RestoranID == restoranid).Select(x => x.Vlasnik.Ime_prezime).FirstOrDefault();
            model.telefon = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.Telefon).FirstOrDefault();
            model.restoranID = restoranid;

            return View(model);
        }


        public IActionResult PrijaviRestoran()
        {
            RestoranPrijavaVM model = new RestoranPrijavaVM();

            
            model.blokovi = context.Blokovi.Include(x=>x.Grad).Select(x => new SelectListItem
            {
                Text = x.Naziv + ", " + x.Grad.Naziv,
                Value = x.BlokID.ToString()

            }).ToList();

            model.vlasnici = context.Vlasnici.Select(x => new SelectListItem
            {
                Text = x.Ime_prezime,
                Value = x.KorisnikID.ToString()

            }).ToList();

            return View(model);
        }

        public IActionResult SnimiRestoran(RestoranPrijavaVM model)
        {

            if (ModelState.IsValid)
            {
                Restoran n = new Restoran
                {
                    BlokID = model.blokId,
                    MinimalnaCijenaNarudžbe = model.minimalnaCijenaNarudzbe,
                    Naziv = model.naziv,
                    Opis = model.opis,
                    Telefon = model.brojTelefona,
                    VlasnikID = model.vlasnikId,

                };
                context.Restorani.Add(n);
                context.SaveChanges();
                HttpContext.SetLogiranogModeratora(HttpContext.GetLogiranogModeratora());
                return RedirectToAction("Index");
            }
            else
            {
                
                model.blokovi = context.Blokovi.Include(x => x.Grad).Select(x => new SelectListItem
                {
                    Text = x.Naziv + ", " + x.Grad.Naziv,
                    Value = x.BlokID.ToString()

                }).ToList();

                model.vlasnici = context.Vlasnici.Select(x => new SelectListItem
                {
                    Text = x.Ime_prezime,
                    Value = x.KorisnikID.ToString()

                }).ToList();
                return View("PrijaviRestoran", model);
            }
        }

        [HttpGet]
        public IActionResult UrediRestoran(int restoranid)
        {
            RestoranUrediVM model = new RestoranUrediVM();
            //model.blokovi = context.Blokovi.Include(x => x.Grad).Select(x => new SelectListItem
            //{
            //    Text = x.Naziv + ", " + x.Grad.Naziv,
            //    Value = x.BlokID.ToString()

            //}).ToList();
            model.blokId= context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.BlokID).FirstOrDefault();
            model.BlokNaziv = context.Restorani.Include(x=>x.Blok).Where(x => x.RestoranID == restoranid).Select(x => x.Blok.Naziv).FirstOrDefault();
            //model.vlasnici = context.Vlasnici.Select(x => new SelectListItem
            //{
            //    Text = x.Ime_prezime,
            //    Value = x.KorisnikID.ToString()

            //}).ToList();
            model.vlasnikNaziv= context.Restorani.Include(x=>x.Vlasnik).Where(x => x.RestoranID == restoranid).Select(x => x.Vlasnik.Ime_prezime).FirstOrDefault();
            model.vlasnikId = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.VlasnikID).FirstOrDefault();
            model.jelovnici = context.Jelovnici.Where(x => x.RestoranID == restoranid).ToList();

            model.brTelefona = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.Telefon).FirstOrDefault();
            model.naziv = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.Naziv).FirstOrDefault();
            model.opis = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.Opis).FirstOrDefault();
            model.minimalnaCijenaNarudz = context.Restorani.Where(x => x.RestoranID == restoranid).Select(x => x.MinimalnaCijenaNarudžbe).FirstOrDefault();
            model.RestoranId = restoranid;

            return View(model);
        }
        [HttpPost]
        public IActionResult UrediRestoran(RestoranUrediVM model)
        {
            if (ModelState.IsValid)
            {
                Restoran n = context.Restorani.Where(x => x.RestoranID == model.RestoranId).FirstOrDefault();
                n.Naziv = model.naziv;
                n.BlokID = model.blokId;
                n.MinimalnaCijenaNarudžbe = model.minimalnaCijenaNarudz;
                n.VlasnikID = model.vlasnikId;
                n.Opis = model.opis;
                n.Telefon = model.brTelefona;

                context.Restorani.Update(n);
                context.SaveChanges();

                return RedirectToAction("UrediRestoran", "Restorani", new { restoranid = model.RestoranId });
            }

            else
            {
                model.blokovi = context.Blokovi.Include(x => x.Grad).Select(x => new SelectListItem
                {
                    Text = x.Naziv + ", " + x.Grad.Naziv,
                    Value = x.BlokID.ToString()

                }).ToList();

                model.vlasnici = context.Vlasnici.Select(x => new SelectListItem
                {
                    Text = x.Ime_prezime,
                    Value = x.KorisnikID.ToString()

                }).ToList();
                model.naziv = context.Restorani.Where(x => x.RestoranID == model.RestoranId).Select(x => x.Naziv).FirstOrDefault();
                model.jelovnici = context.Jelovnici.Where(x => x.RestoranID == model.RestoranId).ToList();
                model.vlasnikId = context.Restorani.Where(x => x.RestoranID == model.RestoranId).Select(x => x.VlasnikID).FirstOrDefault();
                model.blokId = context.Restorani.Where(x => x.RestoranID == model.RestoranId).Select(x => x.BlokID).FirstOrDefault();
                return View("UrediRestoran", model);

            }
        }
        public IActionResult SnimiJelovnik(string opis,bool aktivan,int jelovnikid,int restoranID)
        {
            
        Jelovnik n = context.Jelovnici.Where(x => x.JelovnikID == jelovnikid).FirstOrDefault();
            n.Opis = opis;
            n.Aktivan = aktivan;
            context.Jelovnici.Update(n);
            context.SaveChanges();
            return RedirectToAction("UrediRestoran", "Restorani", new { restoranid = restoranID });
        }

        [HttpPost]
        public IActionResult DodajBlok(string nazivBloka,string nazivGrada,int postanskiBroj)
        {

            Grad n = new Grad
            {
                Naziv = nazivGrada,
                PoštanskiBroj = postanskiBroj

            };
            context.Gradovi.Add(n);

            context.SaveChanges();

            Blok n2 = new Blok
            {
                Naziv = nazivBloka,
                GradID = n.GradID
            };
            context.Blokovi.Add(n2);

            context.SaveChanges();

            return RedirectToAction("PrijaviRestoran", "Restorani");

        }
        [HttpGet]
        public IActionResult DodajJelovnik(int restoranid)
        {
            DodajJelovnikVM model = new DodajJelovnikVM();
            model.restoranid = restoranid;
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult DodajJelovnik(DodajJelovnikVM model)
        {
            Jelovnik n = new Jelovnik
            {
                Aktivan=model.aktivan,
                Opis=model.opis,
                RestoranID=model.restoranid
            };

            context.Jelovnici.Add(n);
            context.SaveChanges();

            return RedirectToAction("UrediRestoran", "Restorani", new { restoranid = model.restoranid });
        }

        public IActionResult ObrisiJelovnik(int jelovnikid)
        {
            int restoranid = context.Jelovnici.Where(x => x.JelovnikID == jelovnikid).Select(x => x.RestoranID).FirstOrDefault();
            Jelovnik n = context.Jelovnici.Where(x => x.JelovnikID == jelovnikid).FirstOrDefault();
            context.Jelovnici.Remove(n);
            context.SaveChanges();

          
            return RedirectToAction(nameof(UrediRestoran), new { restoranid = restoranid });
        }

        public IActionResult ObrisiRestoran(int restoranid)
        {
            Restoran n = context.Restorani.Where(x => x.RestoranID == restoranid).FirstOrDefault();
            context.Restorani.Remove(n);
            context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }


        public IActionResult ValidacijaJelovnik(string opis, int restoranid)
        {
            if (context.Jelovnici.Where(x => x.RestoranID == restoranid).Any(x => x.Opis == opis))
            {
                return Json($"Jelovnik '{opis}' is already in use");
            }
            return Json(true);
        }


        public IActionResult ValidacijaRestoran(string naziv)
        {
            if (context.Restorani.Any(x => x.Naziv == naziv))
            {
                return Json($"restoran '{naziv}' is already in use");
            }
            return Json(true);
        }

        public IActionResult Index(string searchString)

        {

            if (HttpContext.GetLogiranogNarucioca() == null && HttpContext.GetLogiranogVlasnika() == null && HttpContext.GetLogiranogModeratora() == null)
                return RedirectToAction("Index", "Login");


            RestoranIndexVM model = new RestoranIndexVM();

            model.vlasnik = new Vlasnik();

            if (HttpContext.GetLogiranogVlasnika() != null)
            {
                Vlasnik n = HttpContext.GetLogiranogVlasnika();
                model.jeLogiran = (RestoranIndexVM.Logiran)1;
                model.vlasnik = n;
            }

            if (HttpContext.GetLogiranogModeratora() != null)
            {
                model.jeLogiran = (RestoranIndexVM.Logiran)0;


            }

            if (HttpContext.GetLogiranogNarucioca() != null)
            {
                model.jeLogiran = (RestoranIndexVM.Logiran)2;


            }


            HttpContext.Session.Set("logiranKao", ((RestoranIndexVM.Logiran)model.jeLogiran).ToString());

            if (HttpContext.GetLogiranogNarucioca() != null)
            {
                IQueryable<Restoran> searched = context.Restorani.Where(X => X.Naziv != null);
                if (!String.IsNullOrEmpty(searchString))
                {

                    searched = context.Restorani.Where(s => s.Naziv.Contains(searchString));

                    if (searched.Count() == 0)
                    {
                        searched = context.Restorani.Where(z => z.Opis.Contains(searchString));
                    }
                    if (searched.Count() == 0)
                    {
                        searched = context.Restorani.Include(x => x.Blok).Include(x => x.Blok.Grad).Where(z => z.Blok.Naziv.Contains(searchString));
                    }
                    if (searched.Count() == 0)
                    {
                        searched = context.Restorani.Include(x => x.Blok).Include(x => x.Blok.Grad).Where(z => z.Blok.Grad.Naziv.Contains(searchString));
                    }
                }
                model.Rows = searched.Include(x => x.Vlasnik).Include(x => x.Blok).Include(x => x.Blok.Grad).Select(x => new RestoranIndexVM.Row
                {

                    jeLajkan = context.Lajkovi.Where(s => s.NarucilacID == HttpContext.GetLogiranogNarucioca().KorisnikID && s.RestoranID == x.RestoranID).Count() > 0 ? false : true,
                    jeVlasnikRestorana = model.vlasnik.KorisnikID == x.VlasnikID ? true : false,
                    nazivRestorana = x.Naziv,
                    RestoranID = x.RestoranID,
                    brojTelefona = x.Telefon,
                    lokacijaRestorana = x.Blok.Naziv + ", " + x.Blok.Grad.Naziv,
                    minimalnaCijenaNarudzbe = x.MinimalnaCijenaNarudžbe,
                    brojLajkova = context.Lajkovi.Where(y => y.RestoranID == x.RestoranID).Count(),
                    opis = x.Opis,
                    vlasnik = x.Vlasnik.Ime_prezime,
                    radnoVrijeme = context.VrijemeRada.Where(y => y.RestoranID == x.RestoranID).Select(y => new SelectListItem
                    {
                        Text = ((Dani)y.Dan).ToString() + ". " + y.VrijemeOtvaranja.Hours + ":" + y.VrijemeOtvaranja.Minutes + (Convert.ToInt32(y.VrijemeOtvaranja.Minutes) < 10 ? "0" : "") + " - " + y.VrijemeZatvaranja.Hours + ":" + y.VrijemeZatvaranja.Minutes + (Convert.ToInt32(y.VrijemeZatvaranja.Minutes) < 10 ? "0" : "") + " h", 
                        Value = ((int)(y.Dan) + 1).ToString()
                    }).ToList(),
                    radnoVrijemeid= ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek
            }).ToList();
                
            }
            else
                {
                IQueryable<Restoran> searched = context.Restorani.Where(X=>X.Naziv!=null);

                if (!String.IsNullOrEmpty(searchString))
                {
                   
                      searched = context.Restorani.Where(s => s.Naziv.Contains(searchString));

                    if (searched.Count() == 0)
                    {
                        searched = context.Restorani.Where(z => z.Opis.Contains(searchString));
                    }
                    if (searched.Count() == 0)
                    {
                        searched = context.Restorani.Include(x => x.Blok).Include(x => x.Blok.Grad).Where(z => z.Blok.Naziv.Contains(searchString));
                    }
                    if(searched.Count()==0)
                    {
                        searched = context.Restorani.Include(x => x.Blok).Include(x => x.Blok.Grad).Where(z => z.Blok.Grad.Naziv.Contains(searchString));
                    }
                }
                    model.Rows = searched.Include(x => x.Vlasnik).Include(x => x.Blok).Include(x => x.Blok.Grad).Select(x => new RestoranIndexVM.Row
                    {

                        jeVlasnikRestorana = model.vlasnik.KorisnikID == x.VlasnikID ? true : false,
                        nazivRestorana = x.Naziv,
                        RestoranID = x.RestoranID,
                        brojTelefona = x.Telefon,
                        lokacijaRestorana = x.Blok.Naziv + ", " + x.Blok.Grad.Naziv,
                        minimalnaCijenaNarudzbe = x.MinimalnaCijenaNarudžbe,
                        brojLajkova = context.Lajkovi.Where(y => y.RestoranID == x.RestoranID).Count(),
                        opis = x.Opis,
                        vlasnik = x.Vlasnik.Ime_prezime,
                        radnoVrijeme = context.VrijemeRada.Where(y => y.RestoranID == x.RestoranID).Select(y => new SelectListItem
                        {
                            Text = ((Dani)y.Dan).ToString() + ". " + y.VrijemeOtvaranja.Hours + ":" + y.VrijemeOtvaranja.Minutes + (Convert.ToInt32(y.VrijemeOtvaranja.Minutes)<10?"0":"") + " - " + y.VrijemeZatvaranja.Hours +":"+y.VrijemeZatvaranja.Minutes + (Convert.ToInt32(y.VrijemeZatvaranja.Minutes) < 10 ? "0" : "") + " h",
                            Value = ((int)(y.Dan)+1).ToString()
                        }).ToList(),
                        radnoVrijemeid = ((int)DateTime.Now.DayOfWeek == 0) ? 7 : (int)DateTime.Now.DayOfWeek

                    }).ToList();
                
            }




            return View("Index",model);
        }






    }
}