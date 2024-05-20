using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kutuka.Models
{
    public class ParticipacaoModelo
    {
        [Key]
        public int Id_Participacao { get; set; }

        // Chave estrangeira para LeilaoModelo
        public int Id_Leilao { get; set; }

        [ForeignKey(nameof(Id_Leilao))]
        public LeilaoModelo Leilao { get; set; }

        // Chave estrangeira para ClienteModelo
        public int Id_Cliente { get; set; }

        [ForeignKey(nameof(Id_Cliente))]
        public ClienteModelo Cliente { get; set; }

        // Chave estrangeira para ViaturaModelo
        public int Id_Viatura { get; set; }

        [ForeignKey(nameof(Id_Viatura))]
        public ViaturaModelo Viatura { get; set; }

        public decimal Valor { get; set; }
    }
}
