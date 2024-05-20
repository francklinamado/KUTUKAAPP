using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Kutuka.Models
{
    public class ClienteModelo
    {
        [Key]
        public int Id_Cliente { get; set; }
        
        [Required(ErrorMessage = "O nome do cliente é obrigatório.")]
        [StringLength(40)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O Email do cliente é obrigatório.")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "A senha do cliente é obrigatório.")]
        public string Senha { get; set; }
        
        public string? Foto_Perfil { get; set; }

        // Propriedade de navegação para o relacionamento 1:N com ParticipacaoModelo
        public virtual ICollection<ParticipacaoModelo> Participacoes { get; set; }
    }
}