using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Kutuka.Models;
using Kutuka.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kutuka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly KutukaDBContext _context;

        public ClienteController(KutukaDBContext context)
        {
            _context = context;
        }

        public class ClienteRegistroModel
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        public class ClienteEdicaoModel
        {
            public int Id_Cliente { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public string Foto_Perfil { get; set; }
        }

        // Método para criar conta de cliente
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] ClienteRegistroModel clienteRegistro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verifica se o email já está registrado
            if (await _context.Cliente.AnyAsync(c => c.Email == clienteRegistro.Email))
            {
                return BadRequest("Email já registrado.");
            }

            var novoCliente = new ClienteModelo
            {
                Nome = clienteRegistro.Nome,
                Email = clienteRegistro.Email,
                Senha = HashSenha(clienteRegistro.Senha)
            };

            _context.Cliente.Add(novoCliente);
            await _context.SaveChangesAsync();

            // Retorna apenas as informações básicas do cliente
            return Ok(new
            {
                novoCliente.Id_Cliente,
                novoCliente.Nome,
                novoCliente.Email
            });
        }

        // Método para iniciar sessão
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var cliente = await _context.Cliente.SingleOrDefaultAsync(c => c.Email == request.Email);
            if (cliente == null || !VerificaSenha(request.Senha, cliente.Senha))
            {
                return Unauthorized("Credenciais inválidas.");
            }

            return Ok("Login realizado com sucesso.");
        }

        // Método para editar perfil do cliente
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] ClienteEdicaoModel clienteAtualizado)
        {
            if (id != clienteAtualizado.Id_Cliente)
            {
                return BadRequest("ID do cliente não corresponde.");
            }

            var clienteExistente = await _context.Cliente.FindAsync(id);
            if (clienteExistente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            clienteExistente.Nome = clienteAtualizado.Nome;
            clienteExistente.Email = clienteAtualizado.Email;
            clienteExistente.Foto_Perfil = clienteAtualizado.Foto_Perfil;

            if (!string.IsNullOrEmpty(clienteAtualizado.Senha))
            {
                clienteExistente.Senha = HashSenha(clienteAtualizado.Senha);
            }

            _context.Entry(clienteExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Retorna apenas as informações básicas do cliente
            return Ok(new
            {
                clienteExistente.Id_Cliente,
                clienteExistente.Nome,
                clienteExistente.Email
            });
        }

        // Método para apagar a conta do cliente
        [HttpDelete("apagar/{id}")]
        public async Task<IActionResult> Apagar(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok("Conta apagada com sucesso.");
        }

        // Método para hash da senha
        private string HashSenha(string senha)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        // Método para verificar a senha
        private bool VerificaSenha(string senha, string hashArmazenado)
        {
            var parts = hashArmazenado.Split('.', 2);
            var salt = Convert.FromBase64String(parts[0]);
            var hash = parts[1];

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hash == hashed;
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
