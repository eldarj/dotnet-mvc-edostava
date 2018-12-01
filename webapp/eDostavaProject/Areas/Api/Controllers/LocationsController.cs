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

namespace eDostava.Web.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Locations")]
    public class LocationsController : Controller
    {
        private readonly MojContext _context;

        public LocationsController(MojContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public ActionResult GetLocations()
        {
            BlokApiModel model = new BlokApiModel
            {
                Blokovi = _context.Blokovi.Include(b => b.Grad).ToList()
            };

            return Ok(model);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlok([FromRoute] int id)
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