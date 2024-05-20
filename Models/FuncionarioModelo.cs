using System.ComponentModel.DataAnnotations;

namespace Kutuka.Models
{
    public class FuncionarioModelo
    {
        [Key]
        public int Id_Funcionario { get;set; }
        
        [Required(ErrorMessage = "O nome do funcionário é obrigatório.")]
        [StringLength(40)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O Email do funcionário é obrigatório.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A senha do funcionário é obrigatória.")]
        [MinLength(8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        
        public string? Foto_Perfil { get; set; }
        
        // Propriedade de navegação para os leilões criados por este funcionário
        public virtual ICollection<LeilaoModelo> Leiloes { get; set; }
    }
}
