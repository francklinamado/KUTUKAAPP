using Kutuka.Data;
using Kutuka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaturaController : ControllerBase
    {
        private readonly KutukaDBContext _context;

        public ViaturaController(KutukaDBContext context)
        {
            _context = context;
        }

        // GET: api/Viatura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViaturaModelo>>> GetViatura()
        {
            return await _context.Viatura.ToListAsync();
        }

        // GET: api/Viatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ViaturaModelo>> GetViatura(int id)
        {
            var viatura = await _context.Viatura.FindAsync(id);

            if (viatura == null)
            {
                return NotFound();
            }

            return viatura;
        }

        // POST: api/Viatura
        [HttpPost]
        public async Task<ActionResult<ViaturaModelo>> PostViatura(ViaturaCriacaoModel viaturaCriacaoModel)
        {
            var novaViatura = new ViaturaModelo
            {
                Marca = viaturaCriacaoModel.Marca,
                Modelo = viaturaCriacaoModel.Modelo,
                Cor = viaturaCriacaoModel.Cor,
                AnoFabricacao = viaturaCriacaoModel.AnoFabricacao,
                Descricao = viaturaCriacaoModel.Descricao,
                Imagens = viaturaCriacaoModel.Imagens
            };

            _context.Viatura.Add(novaViatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetViatura), new { id = novaViatura.Id_Viatura }, novaViatura);
        }

        // PUT: api/Viatura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViatura(int id, ViaturaEdicaoModel viaturaEdicaoModel)
        {
            var viaturaExistente = await _context.Viatura.FindAsync(id);
            if (viaturaExistente == null)
            {
                return NotFound("Viatura não encontrada.");
            }

            viaturaExistente.Marca = viaturaEdicaoModel.Marca;
            viaturaExistente.Modelo = viaturaEdicaoModel.Modelo;
            viaturaExistente.Cor = viaturaEdicaoModel.Cor;
            viaturaExistente.AnoFabricacao = viaturaEdicaoModel.AnoFabricacao;
            viaturaExistente.Descricao = viaturaEdicaoModel.Descricao;
            viaturaExistente.Imagens = viaturaEdicaoModel.Imagens;

            _context.Entry(viaturaExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(viaturaExistente);
        }

        // DELETE: api/Viatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViatura(int id)
        {
            var viatura = await _context.Viatura.FindAsync(id);
            if (viatura == null)
            {
                return NotFound("Viatura não encontrada.");
            }

            _context.Viatura.Remove(viatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViaturaExists(int id)
        {
            return _context.Viatura.Any(e => e.Id_Viatura == id);
        }
    }
}
