using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class ConsultaDao
    {
        private static Context ctx; //= SingletonContext.GetInstance();


        public static bool CadastrarConsulta(Consulta c)
        {
            if (BuscarConsultaPorid(c.Id) == null)
            {
                ctx.Consultas.Add(c);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<Consulta> ListarConsulta() => ctx.Consultas.Include("Medico").ToList();

        public static List<Consulta> BuscarConsultaPorMedico(int m)
        {
            return ctx.Consultas.Where(x => x.Medico.Id.Equals(m)).ToList();
        }
        
        public static Consulta BuscarConsultaPorid(int c)
        {
            return ctx.Consultas.FirstOrDefault(x => x.Id.Equals(c));
        }

        public static List<Consulta> BucarConsultaPorPaciente(int p)
        {
            return ctx.Consultas.Where(x => x.Paciente.Id.Equals(p)).ToList();
        }

        public static void RemoverConsulta(Consulta c)
        {
            ctx.Consultas.Remove(c);
            ctx.SaveChanges();
        }

        public static void AlterarConsulta(Consulta c)
        {
            ctx.Entry(c).State = EntityState.Modified;
            ctx.SaveChanges();
        }


    }
}
