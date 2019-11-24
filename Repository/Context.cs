using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository
{
    public class Context : IdentityDbContext<UsuarioLogado>
    {
        //public DbSet<Paciente> Paciente { get; set; }
        public Context(DbContextOptions options) 
            : base(options) { }
        public DbSet<Paciente> Produtos { get; set; }
    }
}
