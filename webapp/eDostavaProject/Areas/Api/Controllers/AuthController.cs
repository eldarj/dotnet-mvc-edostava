﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.Api.Models;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : Controller
    {
        private readonly string IMAGE_DIR = "uploads/images/korisnik";
        private readonly string DEFAULT_IMAGE = "default_identicon.png";
        private readonly MojContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public AuthController(MojContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _appEnvironment = hostingEnvironment;
        }

        // POST: api/Auth
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthLoginPost postAccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthUserVM model = await _context.Narucioci
                .Include(n => n.Blok)
                .ThenInclude(n => n.Grad)
                .Where(n => n.Username == postAccount.Username && n.Password == postAccount.Password)
                .Select(s => new AuthUserVM
                {
                    Id = s.KorisnikID,
                    Ime = s.Ime,
                    Prezime = s.Prezime,
                    Username = s.Username,
                    Password = s.Password,
                    Blok = s.Blok,
                    DatumKreiranja = s.DatumKreiranja,
                    ZadnjiLogin = DateTime.Now,
                    Token ="",
                    Adresa = s.Adresa,
                    ImageUrl = s.ImageUrl != null && s.ImageUrl.Length > 0 ? s.ImageUrl : IMAGE_DIR + "/" + DEFAULT_IMAGE,
                })
                .FirstOrDefaultAsync();


            if (model != null)
            {
                // due to lazyloading - fix this later
                model.Blok.Grad = _context.Gradovi.Where(g => g.GradID == model.Blok.GradID).FirstOrDefault();
                return Ok(model);
            }

            return BadRequest("Pogrešan username ili password.");
        }


        // POST: api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterPost Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Model.Id == 0)
            {
                Narucilac newUser = new Narucilac
                {
                    Ime = Model.Ime,
                    Prezime = Model.Prezime,
                    Password = Model.Password,
                    Username = Model.Username,
                    ImageUrl = Model.ImageUrl != null && Model.ImageUrl.Length > 0 ? Model.ImageUrl : IMAGE_DIR + "/" + DEFAULT_IMAGE,
                    Adresa = Model.Adresa,
                    BadgeID = 1,
                    DatumKreiranja = DateTime.Now,
                    Blok = _context.Blokovi.Include(b => b.Grad).DefaultIfEmpty(_context.Blokovi.First()).First(b => b.BlokID == Model.BlokID)
                };
                
                await _context.Narucioci.AddAsync(newUser);
                await _context.SaveChangesAsync();

                AuthUserVM authUserVM = new AuthUserVM
                {
                    Id = newUser.KorisnikID,
                    Ime = newUser.Ime,
                    Prezime = newUser.Prezime,
                    Username = newUser.Username,
                    Password = newUser.Password,
                    Blok = newUser.Blok,
                    DatumKreiranja = newUser.DatumKreiranja,
                    ZadnjiLogin = DateTime.Now,
                    Token = "",
                    Adresa = newUser.Adresa,
                    ImageUrl = newUser.ImageUrl,
                    NarudzbeCount = _context.Narudzbe.Where(n => n.NarucilacID == newUser.KorisnikID).Count()
                };

                return CreatedAtAction("Register", new { id = authUserVM.Id }, authUserVM);
            }

            return BadRequest("Register failed, check what (existing) data are you passing. Did you mean to call the Update action?");
        }

        // POST: api/Auth/Update
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] AuthRegisterPost Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Model.Id != 0)
            {
                Narucilac user = await _context.Narucioci.FindAsync(Model.Id);
                user.Ime = Model.Ime ?? user.Ime;
                user.Prezime = Model.Prezime ?? user.Prezime;
                user.Password = Model.Password ?? user.Password;
                user.Username = Model.Username ?? user.Username;
                user.ImageUrl = Model.ImageUrl != null && Model.ImageUrl.Length > 0 ? Model.ImageUrl : IMAGE_DIR + "/" + DEFAULT_IMAGE;
                user.Adresa = Model.Adresa ?? user.Adresa;
                user.Blok = _context.Blokovi.Include(b => b.Grad).DefaultIfEmpty(_context.Blokovi.First()).First(b => b.BlokID == Model.BlokID);

                await _context.SaveChangesAsync();

                AuthUserVM authUserVM = new AuthUserVM
                {
                    Id = user.KorisnikID,
                    Ime = user.Ime,
                    Prezime = user.Prezime,
                    Username = user.Username,
                    Password = user.Password,
                    Blok = user.Blok,
                    DatumKreiranja = user.DatumKreiranja,
                    ZadnjiLogin = DateTime.Now,
                    Token = "",
                    Adresa = user.Adresa,
                    ImageUrl = user.ImageUrl,
                    NarudzbeCount = _context.Narudzbe.Where(n => n.NarucilacID == user.KorisnikID).Count()
                };

                return CreatedAtAction("Update", new { id = authUserVM.Id }, authUserVM);
            }

            return BadRequest("Update failed, morate proslijediti validan postojeći PK-ID i korisnički račun!");
        }

        // DELETE: api/Auth/Delete
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] AuthRegisterPost Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Narucilac narucilac = await _context.Narucioci.SingleOrDefaultAsync(m => m.KorisnikID == Model.Id);
            if (narucilac == null)
            {
                return NotFound();
            }

            _context.Narucioci.Remove(narucilac);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // UPLOAD PROFILE IMAGE: api/Auth/User/Image
        [HttpPost]
        [Route("User/Image")]
        public async Task<IActionResult> UploadImage([FromBody] UserImagePost Model)
        {
            if (Model.EncodedImageBase64 == null || !(Model.EncodedImageBase64.Length > 0))
            {
                return BadRequest("Nedostaje slika.");
            }

            Narucilac user = await _context.Narucioci.FindAsync(Model.UserId);

            if (user != null)
            {
                try
                {
                    string Filename = Model.FileName + "_" + Model.UserId + "_" + Guid.NewGuid().ToString().Substring(0, 4) + ".jpeg";
                    string Uploads = Path.Combine(_appEnvironment.WebRootPath, IMAGE_DIR);
                    string FilePath = Path.Combine(Uploads, Filename); // Pripremi path i ime slike

                    byte[] imageBytes = Convert.FromBase64String(Model.EncodedImageBase64);
                    System.IO.File.WriteAllBytes(FilePath, imageBytes);

                    user.ImageUrl = IMAGE_DIR + "/" + Filename;
                    await _context.SaveChangesAsync();

                    return Ok(user.ImageUrl);
                }
                catch ( Exception e )
                {
                    // handle ili samo pust da akcija vrati bad request?
                }
            }

            return BadRequest("Dogodila se greska pri konverziji base64 u sliku.");
        }

        private bool NarucilacExists(int id)
        {
            return _context.Narucioci.Any(e => e.KorisnikID == id);
        }
    }
}