using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class CategoriaDao
    {
        private Context ctx; //= SingletonContext.GetInstance();

        public Categoria BuscarCategoriaPorNome
            (Categoria c) =>
            ctx.Categorias.FirstOrDefault
            (x => x.Nome.Equals(c.Nome));

        public List<Categoria> ListarCategoria() => ctx.Categorias.ToList();

        public bool CadastrarCategoria(Categoria c)
        {
            if (BuscarCategoriaPorNome(c) == null)
            {
                ctx.Categorias.Add(c);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public Categoria BuscarCategoriaPorId(int id)
        {
            return ctx.Categorias.Find(id);
        }

    }
}
