using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projeto02.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoDao _medicoDao;
        private readonly CategoriaDao _categoriaDao;
        private readonly DisponibilidadeDao _disponibilidadeDao;
        private Medico _medico;
        //private readonly UserManager<UsuarioLogado> _userManager;
        //private readonly SignInManager<UsuarioLogado> _signInManager;

        public MedicoController(MedicoDao medicoDao, CategoriaDao categoriaDao, DisponibilidadeDao disponibilidadeDao
            //, UserManager<UsuarioLogado> userManager, SignInManager<UsuarioLogado> signInManager
            )
        {
            _medicoDao = medicoDao;
            _categoriaDao = categoriaDao;
            _disponibilidadeDao = disponibilidadeDao;
            //_userManager = userManager;
            //_signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastroMedico()
        {
            ViewBag.Categorias = new SelectList(_categoriaDao.ListarCategoria(), "CategoriaId", "Nome");
            Medico medico = new Medico();

            return View(medico);
        }

        [HttpPost]
        public IActionResult CadastroMedico(Medico m, int drpCategorias)
        {
            ViewBag.Categorias = new SelectList(_categoriaDao.ListarCategoria(), "CategoriaId", "Nome");

            //if (ModelState.IsValid)
            //{
                UsuarioLogado usuarioLogado = new UsuarioLogado
                {
                    UserName = m.Login
                };
            //IdentityResult result = await _userManager.CreateAsync(usuarioLogado, m.Senha);
            //if (result.Succeeded)
            //{
            //await _signInManager.SignInAsync(usuarioLogado, isPersistent: false);
            m.Categoria = _categoriaDao.BuscarCategoriaPorId(drpCategorias);
                    if (_medicoDao.CadastrarMedico(m))
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Este e-mail já está sendo utilizado");
                //}
                //AdicionarErros(result);
            //}
            return View(m);
        }

        public void AdicionarErros(IdentityResult result)
        {
            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        public IActionResult Logout()
        {
            //await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Medico m)
        {
           // var result = await _signInManager.PasswordSignInAsync(m.Login, m.Senha, true, lockoutOnFailure: false);
            //if (result.Succeeded)
           // {
                return RedirectToAction("Index", "Medico");
           // }
           // ModelState.AddModelError("", "Falha no Login");
            //return View();
        }


        public IActionResult DisponibilidadeMedico(int id)
        {
            _medico = _medicoDao.BuscarMedicoPorId(id);
            Disponibilidade disponibilidade = _disponibilidadeDao.BuscarDisponibilidadePorMedico(_medico);
            if (disponibilidade != null)
            {
                disponibilidade = _disponibilidadeDao.BuscarDisponibilidadePorMedico(_medico);
            }
            return View(disponibilidade);
        }

        [HttpPost]
        public IActionResult DisponibilidadeMedico(Disponibilidade disponibilidade)
        {
            //if (ModelState.IsValid)
            //{
            if (disponibilidade.Medico == null)
            {
                disponibilidade.Medico = _medico;
                if (_disponibilidadeDao.CadastrarDisponibilidade(disponibilidade))
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Algum erro aconteceu");
            }
            if (_disponibilidadeDao.AlterarDisponibilidade(disponibilidade))
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Algum erro aconteceu");

            //}
            return View(disponibilidade);
        }
    }
}