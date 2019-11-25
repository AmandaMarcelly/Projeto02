using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    class PacienteDao //: IRepository<Produto>
    {
        private static Context ctx; //= SingletonContext.GetInstance();

        public static bool CadastrarPaciente(Paciente p)
        {
            if (BuscarPacientePorNome(p.Nome) == null)
            {
                ctx.Pacientes.Add(p);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<Paciente> ListarPaciente() => ctx.Pacientes.Include("Medico").ToList();

        public static Paciente BuscarPacientePorNome(string p)
        {
            return ctx.Pacientes.FirstOrDefault(x => x.Nome.Equals(p));
        }

        /*
        public static Paciente BuscarPacientePorLogin(Paciente p)
        {
            return ctx.Pacientes.FirstOrDefault(x => x.Login.Equals(p.Login));
        }*/

        public static List<Paciente> BucarPacientePorParteNome(string p)
        {
            return ctx.Pacientes.Where(x => x.Nome.Contains(p)).ToList();
        }

        public static Paciente BuscarPacientePorId(int id)
        {
            return ctx.Pacientes.Find(id);
        }

        public static void RemoverPaciente(Paciente p)
        {
            ctx.Pacientes.Remove(p);
            ctx.SaveChanges();
        }

        public static void AlterarPaciente(Paciente p)
        {
            ctx.Entry(p).State = EntityState.Modified;
            ctx.SaveChanges();
        }
    }


}
}
