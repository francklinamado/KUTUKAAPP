using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Kutuka.Models
{
    public class LanceModelo
    {
        [Key]
        public int Id_Lance { get; set; }

        // Chave estrangeira para ParticipacaoModelo
        public int Id_Participacao { get; set; }

        [ForeignKey(nameof(Id_Participacao))]
        public virtual ParticipacaoModelo Participacao { get; set; }

        public int Id_Cliente { get; set; }

        [ForeignKey(nameof(Id_Cliente))]
        public ClienteModelo Cliente { get; set; }

        public decimal Valor { get; set; }
    }
}
