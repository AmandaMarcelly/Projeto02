using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace API.Controllers
{
    [Route("api/Consulta")]
    [ApiController]
    public class ConsultaAPIController : ControllerBase
    {

        //  [Route("BuscarPorId/{id}")]
        //    public IActionResult BuscarPorId(int id)

        private readonly MedicoDao _medicoDao;

        public ConsultaAPIController(MedicoDao medicoDao)
        {
            _medicoDao = medicoDao;
        }

        //GET: /api/Consulta/ListarTodos
        [HttpGet]
        [Route("ListarTodos")]
        public IActionResult ListarTodos()
        {
            Console.WriteLine("Hello World!");
            return Ok(_medicoDao.ListarMedico());
        }

        // GET api/values
        [HttpGet]
        [Route("values")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
