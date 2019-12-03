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

        Consulta _consulta = new Consulta();

        public ConsultaController(ConsultaDao consultaDao)
        {
            _consultaDao = consultaDao;
        }

        public IActionResult CadastroConsulta()
        {
            return View();
        }


        public IActionResult AnotacaoSobreConsulta(Consulta consulta)
        {
            _consulta = consulta;
            return View(consulta);
        }

        [HttpPost]
        public IActionResult AnotacaoSobreConsulta(String anotacao)
        {
            _consulta.Anotacao = anotacao;
            if (_consultaDao.AlterarConsulta(_consulta))
            {
                return RedirectToAction("Index");
            }
            return View(_consulta);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}