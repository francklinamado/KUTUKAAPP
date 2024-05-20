using Microsoft.AspNetCore.Mvc;
using Kutuka.Data;
using Kutuka.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Kutuka.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly KutukaDBContext _context;

        public FuncionarioController(KutukaDBContext context)
        {
            _context = context;
        }

        public class FuncionarioRegistroModel
        {
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        public class FuncionarioEdicaoModel
        {
            public int Id_Funcionario { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
        }

        // Método para criar conta de funcionário
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] FuncionarioRegistroModel funcionarioRegistro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Verifica se o email já está registrado
            if (await _context.Funcionario.AnyAsync(f => f.Email == funcionarioRegistro.Email))
            {
                return BadRequest("Email já registrado.");
            }

            // Hash da senha
            var hashedSenha = HashSenha(funcionarioRegistro.Senha);

            var novoFuncionario = new FuncionarioModelo
            {
                Nome = funcionarioRegistro.Nome,
                Email = funcionarioRegistro.Email,
                Senha = hashedSenha
            };

            _context.Funcionario.Add(novoFuncionario);
            await _context.SaveChangesAsync();

            // Retorna apenas as informações básicas do funcionário
            return Ok(new
            {
                novoFuncionario.Id_Funcionario,
                novoFuncionario.Nome,
                novoFuncionario.Email
            });
        }

        // Método para iniciar sessão
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] FuncionarioControllerLoginRequest request)
        {
            var funcionario = await _context.Funcionario.SingleOrDefaultAsync(f => f.Email == request.Email);
            if (funcionario == null || !VerificaSenha(request.Senha, funcionario.Senha))
            {
                return Unauthorized("Credenciais inválidas.");
            }

            // Aqui, você pode gerar um token de autenticação e retorná-lo (JWT, por exemplo)
            return Ok("Login realizado com sucesso.");
        }

        // Método para editar perfil do funcionário
        [HttpPut("editar/{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] FuncionarioEdicaoModel funcionarioAtualizado)
        {
            if (id != funcionarioAtualizado.Id_Funcionario)
            {
                return BadRequest("ID do funcionário não corresponde.");
            }

            var funcionarioExistente = await _context.Funcionario.FindAsync(id);
            if (funcionarioExistente == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            funcionarioExistente.Nome = funcionarioAtualizado.Nome;
            funcionarioExistente.Email = funcionarioAtualizado.Email;

            if (!string.IsNullOrEmpty(funcionarioAtualizado.Senha))
            {
                funcionarioExistente.Senha = HashSenha(funcionarioAtualizado.Senha);
            }

            _context.Entry(funcionarioExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Retorna apenas as informações básicas do funcionário
            return Ok(new
            {
                funcionarioExistente.Id_Funcionario,
                funcionarioExistente.Nome,
                funcionarioExistente.Email
            });
        }

        // Método para apagar a conta do funcionário
        [HttpDelete("apagar/{id}")]
        public async Task<IActionResult> Apagar(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound("Funcionário não encontrado.");
            }

            _context.Funcionario.Remove(funcionario);
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

    // Modelo para receber login via HTTP
    public class FuncionarioControllerLoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
