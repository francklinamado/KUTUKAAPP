using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kutuka.Models
{
    public class LeilaoModelo
    {
        [Key]
        public int Id_Leilao { get;set; }
        
        // Chave estrangeira para FuncionarioModelo
        public int Id_Funcionario { get;set; }

        [ForeignKey(nameof(Id_Funcionario))]
        public FuncionarioModelo Funcionario { get;set; }

        // Data de início do leilão
        public DateTime Data_Inicio { get;set; }
        
        // Data de fim do leilão
        public DateTime Data_Fim { get;set; }
        public decimal PrecoInicial {get; set; } 
        // Estado do leilão
        [StringLength(8)]
        public string Estado {get; set; }
        
        // Descrição do leilão
        public string? Descricao { get; set; }
        
        // Propriedade de navegação para participações associadas a este leilão
        public virtual ICollection<ParticipacaoModelo> Participacoes { get; set; }
    }
}
