using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;


namespace Projeto02.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ConsultaDao _consultaDao;

        public ConsultaController(ConsultaDao consultaDao)
        {
            _consultaDao = consultaDao;
        }

        public IActionResult AnotacaoSobreConsulta()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}