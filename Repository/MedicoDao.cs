using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class MedicoDao : IRepository<Medico>
    {
        private readonly Context ctx; //= SingletonContext.GetInstance();

        public MedicoDao (Context context)
        {
            ctx = context;
        }

        public bool CadastrarMedico(Medico m)
        {
            if (BuscarMedicoPorNome(m) == null)
            {
                ctx.Medicos.Add(m);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Medico> ListarMedico()
        {
            return ctx.Medicos.ToList();
        }
        
        public List<Medico> BuscarMedicosPorCategoria(int CategoriaId)
        {
            return ctx.Medicos.Where(x => x.Categoria.CategoriaId.Equals(CategoriaId)).ToList();
        }
        public Medico BuscarMedicoPorNome(Medico m)
        {
            return ctx.Medicos.FirstOrDefault(x => x.Nome.Equals(m.Nome));
        }

        public Medico BuscarMedicoPorLogin(string login)
        {
            return ctx.Medicos.FirstOrDefault(x => x.Login.Equals(login));
        }

        public List<Medico> BucarMedicoPorParteNome(string m)
        {
            return ctx.Medicos.Where(x => x.Nome.Contains(m)).ToList();
        }

        public Medico BuscarMedicoPorId(int id)
        {
            return ctx.Medicos.Find(id);
        }

        public void RemoverMedico(Medico m)
        {
            ctx.Medicos.Remove(m);
            ctx.SaveChanges();
        }

        public void AlterarMedico(Medico m)
        {
            ctx.Entry(m).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public bool Cadastrar(Medico m)
        {
            if (BuscarMedicoPorNome(m) == null)
            {
                ctx.Medicos.Add(m);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Medico> ListarTodos()
        {
            return ctx.Medicos.Include(x => x.Categoria).ToList();
        }

        public Medico BuscarPorId(int? id)
        {
            return ctx.Medicos.Find(id);
        }
    }
}
