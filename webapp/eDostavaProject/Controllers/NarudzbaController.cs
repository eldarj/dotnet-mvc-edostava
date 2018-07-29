using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eDostava.Data;
using eDostava.Data.Models;

namespace eDostava.Web.Controllers
{
    public class NarudzbaController : Controller
    {
        private MojContext conext;
        public NarudzbaController(MojContext db)
        {
            conext = db;
        }

        // GET: Narudzba
        public async Task<IActionResult> Index()
        {
            var mojContext = conext.Narudzbe.Include(n => n.Kupon);
            return View(await mojContext.ToListAsync());
        }

        // GET: Narudzba/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await conext.Narudzbe
                .Include(n => n.Kupon)
                .SingleOrDefaultAsync(m => m.NarudzbaID == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: Narudzba/Create
        public IActionResult Create()
        {
            ViewData["KuponID"] = new SelectList(conext.Kuponi, "KuponID", "KuponID");
            return View();
        }

        // POST: Narudzba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NarudzbaID,Sifra,DatumVrijeme,UkupnaCijena,Status,NarucilacID,KuponID")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                conext.Add(narudzba);
                await conext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KuponID"] = new SelectList(conext.Kuponi, "KuponID", "KuponID", narudzba.KuponID);
            return View(narudzba);
        }

        // GET: Narudzba/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await conext.Narudzbe.SingleOrDefaultAsync(m => m.NarudzbaID == id);
            if (narudzba == null)
            {
                return NotFound();
            }
            ViewData["KuponID"] = new SelectList(conext.Kuponi, "KuponID", "KuponID", narudzba.KuponID);
            return View(narudzba);
        }

        // POST: Narudzba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NarudzbaID,Sifra,DatumVrijeme,UkupnaCijena,Status,NarucilacID,KuponID")] Narudzba narudzba)
        {
            if (id != narudzba.NarudzbaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    conext.Update(narudzba);
                    await conext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.NarudzbaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KuponID"] = new SelectList(conext.Kuponi, "KuponID", "KuponID", narudzba.KuponID);
            return View(narudzba);
        }

        // GET: Narudzba/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await conext.Narudzbe
                .Include(n => n.Kupon)
                .SingleOrDefaultAsync(m => m.NarudzbaID == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: Narudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzba = await conext.Narudzbe.SingleOrDefaultAsync(m => m.NarudzbaID == id);
            conext.Narudzbe.Remove(narudzba);
            await conext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(int id)
        {
            return conext.Narudzbe.Any(e => e.NarudzbaID == id);
        }
    }
}
