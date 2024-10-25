using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UESAN.StoreDB.DOMAIN.Core.Entities;
using UESAN.StoreDB.DOMAIN.Infrastructure.Data;

namespace PROMCOSER.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MaquinariasController : ControllerBase
    {
        
        private readonly PromcoserContext _context;

        public MaquinariasController(PromcoserContext context)
        {
            _context = context;
        }
        [Authorize]

        // GET: api/Maquinarias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquinaria>>> GetMaquinaria()
        {
            return await _context.Maquinaria.ToListAsync();
        }

        // GET: api/Maquinarias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Maquinaria>> GetMaquinaria(long id)
        {
            var maquinaria = await _context.Maquinaria.FindAsync(id);

            if (maquinaria == null)
            {
                return NotFound();
            }

            return maquinaria;
        }

        // PUT: api/Maquinarias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquinaria(long id, Maquinaria maquinaria)
        {
            if (id != maquinaria.IdMaquinaria)
            {
                return BadRequest();
            }

            _context.Entry(maquinaria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinariaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Maquinarias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maquinaria>> PostMaquinaria(Maquinaria maquinaria)
        {
            _context.Maquinaria.Add(maquinaria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMaquinaria", new { id = maquinaria.IdMaquinaria }, maquinaria);
        }

        // DELETE: api/Maquinarias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquinaria(long id)
        {
            var maquinaria = await _context.Maquinaria.FindAsync(id);
            if (maquinaria == null)
            {
                return NotFound();
            }

            _context.Maquinaria.Remove(maquinaria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaquinariaExists(long id)
        {
            return _context.Maquinaria.Any(e => e.IdMaquinaria == id);
        }
    }
}
