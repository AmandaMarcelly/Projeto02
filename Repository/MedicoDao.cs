using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    class MedicoDao
    {
        private static Context ctx; //= SingletonContext.GetInstance();

        public static bool CadastrarMedico(Medico m)
        {
            if (BuscarMedicoPorNome(m) == null)
            {
                ctx.Medicos.Add(m);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static List<Medico> ListarMedico() => ctx.Medicos.Include("Especialidade").ToList();

        public static Medico BuscarMedicoPorEspecialidade(Medico m)
        {
            return ctx.Medicos.FirstOrDefault(x => x.Especialidade.Equals(m.Especialidade));
        }
        public static List<Medico> BuscarMedicosPorCategoria(int CategoriaId)
        {
            return ctx.Medicos.Where(x => x.Categoria.CategoriaId.Equals(CategoriaId)).ToList();
        }
        public static Medico BuscarMedicoPorNome(Medico m)
        {
            return ctx.Medicos.FirstOrDefault(x => x.Nome.Equals(m.Nome));
        }

        /*public static Medico BuscarMedicoPorLogin(Medico m)
        {
            return ctx.Medicos.FirstOrDefault(x => x.Login.Equals(m.Login));
        }
        */

        public static List<Medico> BucarMedicoPorParteNome(string m)
        {
            return ctx.Medicos.Where(x => x.Nome.Contains(m)).ToList();
        }

        public static Medico BuscarMedicoPorId(int id)
        {
            return ctx.Medicos.Find(id);
        }

        public static void RemoverMedico(Medico m)
        {
            ctx.Medicos.Remove(m);
            ctx.SaveChanges();
        }

        public static void AlterarMedico(Medico m)
        {
            ctx.Entry(m).State = EntityState.Modified;
            ctx.SaveChanges();
        }




    }
}
