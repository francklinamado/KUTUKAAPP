using Microsoft.AspNetCore.Mvc;
using Kutuka.Data;
using Kutuka.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Kutuka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Funcionario")]
    public class LeilaoController : ControllerBase
    {
        private readonly KutukaDBContext _context;

        public LeilaoController(KutukaDBContext context)
        {
            _context = context;
        }

        public class LeilaoCriacaoModel
        {
            public DateTime Data_Inicio { get; set; }
            public DateTime Data_Fim { get; set; }
            public string Estado { get; set; }
            public string Descricao { get; set; }
        }

        public class LeilaoEdicaoModel
        {
            public DateTime Data_Inicio { get; set; }
            public DateTime Data_Fim { get; set; }
            public string Estado { get; set; }
            public string Descricao { get; set; }
        }

        // Método para criar um leilão
        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] LeilaoCriacaoModel leilao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var funcionarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var novoLeilao = new LeilaoModelo
            {
                Id_Funcionario = funcionarioId,
                Data_Inicio = leilao.Data_Inicio,
                Data_Fim = leilao.Data_Fim,
                Estado = leilao.Estado,
                Descricao = leilao.Descricao
            };

            _context.Leilao.Add(novoLeilao);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                novoLeilao.Id_Leilao,
                novoLeilao.Data_Inicio,
                novoLeilao.Data_Fim,
                novoLeilao.Estado,
                novoLeilao.Descricao
            });
        }

        // Método para editar um leilão
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] LeilaoEdicaoModel leilaoAtualizado)
        {
            var leilaoExistente = await _context.Leilao.FindAsync(id);
            if (leilaoExistente == null)
            {
                return NotFound("Leilão não encontrado.");
            }

            var funcionarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (leilaoExistente.Id_Funcionario != funcionarioId)
            {
                return Unauthorized("Você não tem permissão para editar este leilão.");
            }

            leilaoExistente.Data_Inicio = leilaoAtualizado.Data_Inicio;
            leilaoExistente.Data_Fim = leilaoAtualizado.Data_Fim;
            leilaoExistente.Estado = leilaoAtualizado.Estado;
            leilaoExistente.Descricao = leilaoAtualizado.Descricao;

            _context.Entry(leilaoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new
            {
                leilaoExistente.Id_Leilao,
                leilaoExistente.Data_Inicio,
                leilaoExistente.Data_Fim,
                leilaoExistente.Estado,
                leilaoExistente.Descricao
            });
        }

        // Método para apagar um leilão
        [HttpDelete("apagar/{id}")]
        public async Task<IActionResult> Apagar(int id)
        {
            var leilao = await _context.Leilao.FindAsync(id);
            if (leilao == null)
            {
                return NotFound("Leilão não encontrado.");
            }

            var funcionarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (leilao.Id_Funcionario != funcionarioId)
            {
                return Unauthorized("Você não tem permissão para apagar este leilão.");
            }

            _context.Leilao.Remove(leilao);
            await _context.SaveChangesAsync();

            return Ok("Leilão apagado com sucesso.");
        }

        // Método para obter todos os leilões do funcionário autenticado
        [HttpGet("meus-leiloes")]
        public async Task<IActionResult> MeusLeiloes()
        {
            var funcionarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var leiloes = await _context.Leilao.Where(l => l.Id_Funcionario == funcionarioId).ToListAsync();

            return Ok(leiloes);
        }
    }
}
