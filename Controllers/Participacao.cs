using Kutuka.Data;
using Kutuka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipacaoController : ControllerBase
    {
        private readonly KutukaDBContext _context;

        public ParticipacaoController(KutukaDBContext context)
        {
            _context = context;
        }

        public class ParticipacaoCriacaoModel
        {
            public int Id_Leilao { get; set; }
            public int Id_Cliente { get; set; }
            public int Id_Viatura { get; set; }
            public decimal Valor { get; set; }
        }

        public class ParticipacaoEdicaoModel
        {
            public decimal Valor { get; set; }
        }

        // GET: api/Participacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParticipacaoModelo>>> GetParticipacao()
        {
            return await _context.Participacao.ToListAsync();
        }

        // GET: api/Participacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ParticipacaoModelo>> GetParticipacao(int id)
        {
            var participacao = await _context.Participacao.FindAsync(id);

            if (participacao == null)
            {
                return NotFound();
            }

            return participacao;
        }

        // POST: api/Participacao
        [HttpPost]
        public async Task<ActionResult<ParticipacaoModelo>> PostParticipacao(ParticipacaoCriacaoModel participacao)
        {
            var novaParticipacao = new ParticipacaoModelo
            {
                Id_Leilao = participacao.Id_Leilao,
                Id_Cliente = participacao.Id_Cliente,
                Id_Viatura = participacao.Id_Viatura,
                Valor = participacao.Valor
            };

            _context.Participacao.Add(novaParticipacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParticipacao), new { id = novaParticipacao.Id_Participacao }, novaParticipacao);
        }

        // PUT: api/Participacao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipacao(int id, ParticipacaoEdicaoModel participacaoAtualizada)
        {
            var participacaoExistente = await _context.Participacao.FindAsync(id);
            if (participacaoExistente == null)
            {
                return NotFound("Participação não encontrada.");
            }

            participacaoExistente.Valor = participacaoAtualizada.Valor;

            _context.Entry(participacaoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Participacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipacao(int id)
        {
            var participacao = await _context.Participacao.FindAsync(id);
            if (participacao == null)
            {
                return NotFound("Participação não encontrada.");
            }

            _context.Participacao.Remove(participacao);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
