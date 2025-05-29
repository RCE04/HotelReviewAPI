using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalCursoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalCursoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LugaresController : ControllerBase
    {
        private readonly HotelReviewContext _context;

        public LugaresController(HotelReviewContext context)
        {
            _context = context;
        }

        // GET: api/Lugares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lugare>>> GetLugares()
        {
            return await _context.Lugares.ToListAsync();
        }

        // GET: api/Lugares/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lugare>> GetLugare(int id)
        {
            var lugar = await _context.Lugares.FindAsync(id);

            if (lugar == null)
            {
                return NotFound();
            }

            return lugar;
        }

        // POST: api/Lugares
        [HttpPost]
        public async Task<ActionResult<Lugare>> PostLugare(Lugare lugar)
        {
            _context.Lugares.Add(lugar);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLugare), new { id = lugar.Id }, lugar);
        }

        // PUT: api/Lugares/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLugare(int id, Lugare lugar)
        {
            if (id != lugar.Id)
            {
                return BadRequest();
            }

            _context.Entry(lugar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LugareExists(id))
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

        // DELETE: api/Lugares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLugare(int id)
        {
            var lugar = await _context.Lugares.FindAsync(id);
            if (lugar == null)
            {
                return NotFound();
            }

            _context.Lugares.Remove(lugar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("imagen-proxy")]
        public async Task<IActionResult> ObtenerImagenProxy([FromQuery] string url)
        {
            using var httpClient = new HttpClient();
            try
            {
                var imagenBytes = await httpClient.GetByteArrayAsync(url);
                // Puedes mejorar esto detectando el content-type dinámicamente si quieres
                return File(imagenBytes, "image/jpeg");
            }
            catch
            {
                return NotFound();
            }
        }

        private bool LugareExists(int id)
        {
            return _context.Lugares.Any(e => e.Id == id);
        }
    }
}