using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    class CategoriaDao
    {
        private static Context ctx; //= SingletonContext.GetInstance();

        public static Categoria BuscarCategoriaPorNome
            (Categoria c) =>
            ctx.Categorias.FirstOrDefault
            (x => x.Nome.Equals(c.Nome));

        public static List<Categoria> ListarCategoria() => ctx.Categorias.ToList();

        public static bool CadastrarCategoria(Categoria c)
        {
            if (BuscarCategoriaPorNome(c) == null)
            {
                ctx.Categorias.Add(c);
                ctx.SaveChanges();
                return true;
            }
            return false;
        }

        public static Categoria BuscarCategoriaPorId(int id)
        {
            return ctx.Categorias.Find(id);
        }

    }
}
