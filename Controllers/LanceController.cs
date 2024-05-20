using Kutuka.Data;
using Kutuka.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuka.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LanceController : ControllerBase
    {
        private readonly KutukaDBContext _context;

        public LanceController(KutukaDBContext context)
        {
            _context = context;
        }

        // POST: api/Lance/lance
        [HttpPost("lance")]
        public async Task<IActionResult> ReceberLance([FromBody] ReceberLanceModel modelo)
        {
            var leilao = await _context.Leilao.FindAsync(modelo.Id_Leilao);
            if (leilao == null)
            {
                return NotFound("Leilão não encontrado.");
            }

            if (leilao.Estado != "Aberto" || DateTime.Now > leilao.Data_Fim)
            {
                return BadRequest("Leilão fechado ou já expirado.");
            }

            var cliente = await _context.Cliente.FindAsync(modelo.Id_Cliente);
            if (cliente == null)
            {
                return BadRequest("Cliente não encontrado.");
            }

            var lanceMaisAlto = _context.Lance
                .Where(l => l.Participacao.Id_Leilao == modelo.Id_Leilao)
                .OrderByDescending(l => l.Valor)
                .FirstOrDefault();

            if (modelo.Valor <= (lanceMaisAlto?.Valor ?? leilao.PrecoInicial))
            {
                return BadRequest("O valor do lance deve ser maior que o lance atual.");
            }

            var participacao = await _context.Participacao
                .FirstOrDefaultAsync(p => p.Id_Leilao == modelo.Id_Leilao && p.Id_Cliente == modelo.Id_Cliente);
            if (participacao == null)
            {
                participacao = new ParticipacaoModelo
                {
                    Id_Leilao = modelo.Id_Leilao,
                    Id_Cliente = modelo.Id_Cliente
                };
                _context.Participacao.Add(participacao);
                await _context.SaveChangesAsync();
            }

            var lance = new LanceModelo
            {
                Id_Participacao = participacao.Id_Participacao,
                Id_Cliente = modelo.Id_Cliente,
                Valor = modelo.Valor
            };

            _context.Lance.Add(lance);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Lance registrado com sucesso.", lance });
        }

        // POST: api/Lance/finalizar/5
        [HttpPost("finalizar/{id}")]
        public async Task<IActionResult> FinalizarLeilao(int id)
        {
            var leilao = await _context.Leilao.FindAsync(id);
            if (leilao == null)
            {
                return NotFound("Leilão não encontrado.");
            }

            if (DateTime.Now <= leilao.Data_Fim)
            {
                return BadRequest("Leilão ainda não expirou.");
            }

            leilao.Estado = "Fechado";

            var lanceGanhador = _context.Lance
                .Where(l => l.Participacao.Id_Leilao == id)
                .OrderByDescending(l => l.Valor)
                .FirstOrDefault();

            if (lanceGanhador != null)
            {
                var clienteGanhador = await _context.Cliente.FindAsync(lanceGanhador.Id_Cliente);
                // Aqui você pode implementar a lógica de notificação para o cliente ganhador, por exemplo, enviar um e-mail.
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Leilão finalizado com sucesso.", ganhador = lanceGanhador });
        }
    }

    // Modelo para receber lance via HTTP POST
    public class ReceberLanceModel
    {
        public int Id_Leilao { get; set; }
        public int Id_Cliente { get; set; }
        public decimal Valor { get; set; }
    }
}
