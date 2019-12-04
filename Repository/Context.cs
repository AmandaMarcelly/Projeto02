using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Repository
{
    public class Context : IdentityDbContext<UsuarioLogado>
    {
        public Context()
        {
        }

        //public DbSet<Paciente> Paciente { get; set; }
        public Context(DbContextOptions options) 
            : base(options) { }
        public DbSet<Paciente> Pacientes { get; set; }

        public DbSet<Medico> Medicos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Consulta> Consultas { get; set; }

        public DbSet<Disponibilidade> Disponibilidades { get; set; }



    }
}
