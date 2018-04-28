using fp_18_web_aula_1_core.Identity;
using fp_web_aula_1_core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace fp_web_aula_1_core.Data
{
    public class CopaContext : IdentityDbContext<ApplicationUser>
    {
        public CopaContext(DbContextOptions<CopaContext> options) :
            base(options)
        {

        }

        public DbSet<Time> Times { get; set; }
        public DbSet<Jogador> Jogadores { get; set; }
    }
}
