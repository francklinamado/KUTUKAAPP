using Kutuka.Models;
using Microsoft.EntityFrameworkCore;

namespace Kutuka.Data 
{
    public class KutukaDBContext: DbContext 
    {
        public KutukaDBContext(DbContextOptions<KutukaDBContext> options) 
            : base(options)
        {

        }

        public DbSet<ClienteModelo> Cliente { get; set; }
        public DbSet<FuncionarioModelo> Funcionario { get; set; }
        public DbSet<LanceModelo> Lance { get; set; }
        public DbSet<LeilaoModelo> Leilao { get; set; }
        public DbSet<ParticipacaoModelo> Participacao { get; set; }
        public DbSet<ViaturaModelo> Viatura { get; set; }
        
        
    }
}
