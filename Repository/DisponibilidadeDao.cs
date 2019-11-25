using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    class DisponibilidadeDao
    {
        private static Context ctx; //= SingletonContext.GetInstance();

        public static bool CadastrarDisponibilidade(Disponibilidade d)
        {

            if (BuscarDisponibilidadePorMedico(d.Medico) == null)
            {
                ctx.Disponibilidades.Add(d);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<Disponibilidade> ListarDisponiblidade() => ctx.Disponibilidades.Include("Medico").ToList();

        public static Disponibilidade BuscarDisponibilidadePorMedico(Medico m)
        {
            return ctx.Disponibilidades.FirstOrDefault(x => x.Medico.Id.Equals(m.Id));
        }

        public static void RemoverDisponbilidade(Disponibilidade d)
        {
            ctx.Disponibilidades.Remove(d);
            ctx.SaveChanges();
        }
        public static void AlterarDisponibilidade(Disponibilidade d)
        {
            ctx.Entry(d).State = EntityState.Modified;
            ctx.SaveChanges();
        }


    }
}
