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
    public class UsuariosController : ControllerBase
    {
        private readonly HotelReviewContext _context;

        public UsuariosController(HotelReviewContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }

        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Usuarios/login
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] Usuario loginData)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u =>
                    u.NombreUsuario == loginData.NombreUsuario &&
                    u.Contraseña == loginData.Contraseña);

            if (usuario == null)
            {
                return Unauthorized();
            }

            return Ok(usuario);
        }

        // ✅ Este es el único método para agregar a favoritos
        [HttpPost("{usuarioId}/favorito/{lugarId}")]
        public async Task<IActionResult> AgregarAFavoritos(int usuarioId, int lugarId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var favoritos = (usuario.Favoritos ?? "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (!favoritos.Contains(lugarId))
            {
                favoritos.Add(lugarId);
                usuario.Favoritos = string.Join(",", favoritos);
                await _context.SaveChangesAsync();
            }

            return Ok(usuario.Favoritos);
        }

        // GET: api/Usuarios/5/favoritos
        [HttpGet("{usuarioId}/favoritos")]
        public async Task<ActionResult<IEnumerable<Lugare>>> ObtenerFavoritos(int usuarioId)
        {
            var usuario = await _context.Usuarios.FindAsync(usuarioId);
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Favoritos))
            {
                return Ok(new List<Lugare>());
            }

            var ids = usuario.Favoritos
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            var lugares = await _context.Lugares
                .Where(l => ids.Contains(l.Id))
                .ToListAsync();

            return Ok(lugares);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
