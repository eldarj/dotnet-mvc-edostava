using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eDostava.Data;
using eDostava.Data.Models;
using eDostava.Web.Areas.Api.Models;
using eDostava.Web.Areas.Api.Helper;

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Lokacije")]
    public class LokacijeController : MyBaseApiController
    {
        public LokacijeController(MojContext context) : base(context) { }

        // GET: api/Lokacije
        [HttpGet]
        public ActionResult GetBlokovi()
        {
            BlokListResponse model = new BlokListResponse
            {
                Blokovi = _context.Blokovi.Include(b => b.Grad).ToList()
            };

            return Ok(model);
        }

        // GET: api/Lokacije/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleBlok([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blok = await _context.Blokovi.SingleOrDefaultAsync(m => m.BlokID == id);

            if (blok == null)
            {
                return NotFound();
            }

            return Ok(blok);
        }
    }
}