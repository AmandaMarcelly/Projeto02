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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (_categoriaDao.CadastrarCategoria(categoria))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Essa categoria já existe!");
            }
            return (View(categoria));
        }
    }
}