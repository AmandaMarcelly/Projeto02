using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Domain;

namespace APIProjeto02.Controllers
{
    [Route("api/Projeto")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MedicoDao _medicoDao;
        private readonly CategoriaDao _categoriaDao;

        public ValuesController(MedicoDao medicoDao, CategoriaDao categoriaDao)
        {
            _medicoDao = medicoDao;
            _categoriaDao = categoriaDao;
        }

        // GET api/values
        [HttpGet]
        [Route("ListarTodos")]
        public ActionResult<IEnumerable<Medico>> ListarTodos()
        {
            return Ok(_medicoDao.ListarMedico());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Route("BuscarPorId/{id}")]
        public ActionResult<string> BuscarPorId(int id)
        {
            return Ok(_medicoDao.BuscarMedicoPorId(id));
        }

        // POST api/values
        [HttpPost]
        [Route("CadastrarCategoria")]
        public IActionResult Cadastrar([FromBody] Categoria c)
        {
            if (ModelState.IsValid)
            {
                if (_categoriaDao.Cadastrar(c))
                {
                    return Created("", c);
                }
                return Conflict(new { msg = "Essa categoria já existe!" });
            }
            return BadRequest(ModelState);
        }

        //GET: /api/Produto/BuscarPorCategoria/2
        [HttpGet]
        [Route("BuscarPorCategoria/{id}")]
        public IActionResult BuscarPorCategoria([FromRoute] int id)
        {
            List<Medico> produtos =
                _medicoDao.BuscarMedicosPorCategoria(id);
            if (produtos.Count > 0)
            {
                return Ok(produtos);
            }
            return NotFound(new { msg = "Essa busca não encontrou nenhum resultado!" });
        }
    }
}
