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
    public class ComentariosController : ControllerBase
    {
        private readonly HotelReviewContext _context;

        public ComentariosController(HotelReviewContext context)
        {
            _context = context;
        }

        // GET: api/Comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios()
        {
            return await _context.Comentarios.ToListAsync();
        }

        // GET: api/Comentarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return comentario;
        }

        // POST: api/Comentarios
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComentario), new { id = comentario.Id }, comentario);
        }

        // PUT: api/Comentarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            if (id != comentario.Id)
            {
                return BadRequest();
            }

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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

        // DELETE: api/Comentarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Comentarios/lugar/5
        [HttpGet("lugar/{lugarId}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosPorLugar(int lugarId)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.LugarId == lugarId)
                .ToListAsync();

            if (comentarios == null || comentarios.Count == 0)
            {
                return NotFound();
            }

            return comentarios;
        }


        // DELETE: api/Comentarios/usuario/5
        [HttpDelete("usuario/{usuarioId}")]
        public async Task<IActionResult> DeleteComentariosPorUsuario(int usuarioId)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.UsuarioId == usuarioId)
                .ToListAsync();

            if (comentarios == null || comentarios.Count == 0)
            {
                return NotFound();
            }

            _context.Comentarios.RemoveRange(comentarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Comentarios/lugar/5
        [HttpDelete("lugar/{lugarId}")]
        public async Task<IActionResult> DeleteComentariosPorLugar(int lugarId)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.LugarId == lugarId)
                .ToListAsync();

            if (comentarios == null || comentarios.Count == 0)
            {
                return NotFound();
            }

            _context.Comentarios.RemoveRange(comentarios);  
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentarioExists(int id)
        {
            return _context.Comentarios.Any(e => e.Id == id);
        }
    }
}