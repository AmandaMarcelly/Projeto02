using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;

namespace Projeto02.Controllers
{
    public class DisponibilidadeController : Controller
    {
        private readonly DisponibilidadeDao _disponibilidadeDao;

        public DisponibilidadeController(DisponibilidadeDao disponibilidadeDao)
        {
            _disponibilidadeDao = disponibilidadeDao;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Disponibilidade disponibilidade)
        {
            if (ModelState.IsValid)
            {
                if (_disponibilidadeDao.CadastrarDisponibilidade(disponibilidade))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Algum erro aconteceu");
            }
            return View(disponibilidade);
        }
    }
}