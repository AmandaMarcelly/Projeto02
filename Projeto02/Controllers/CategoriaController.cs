using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;

namespace Projeto02.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly CategoriaDao _categoriaDao;

        public CategoriaController(CategoriaDao categoriaDao)
        { 
            _categoriaDao = categoriaDao;
        }

        public IActionResult Index()
        {
            return View(_categoriaDao.ListarCategoria());
        }

        public IActionResult CadastroCategoria()
        {
            Categoria categoria = new Categoria();
            return View(categoria);
        }

        [HttpPost]
        public IActionResult CadastroCategoria(Categoria categoria)
        {
            //if (ModelState.IsValid)
            //{
                if (_categoriaDao.CadastrarCategoria(categoria))
                {
                    return RedirectToAction("MenuPrincipalMedico", "Medico");
                }
                ModelState.AddModelError("", "Essa categoria já existe!");
            //}
            return (View(categoria));
        }
    }
}