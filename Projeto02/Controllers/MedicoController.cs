using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;
using Microsoft.AspNetCore.Identity;

namespace Projeto02.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoDao _medicoDao;
        private readonly UserManager<UsuarioLogado> _userManager;
        private readonly SignInManager<UsuarioLogado> _signInManager;

        public MedicoController(MedicoDao medicoDao, UserManager<UsuarioLogado> userManager, SignInManager<UsuarioLogado> signInManager)
        {
            _medicoDao = medicoDao;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            Medico medico = new Medico();

            return View(medico);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(Medico m)
        {
            if (ModelState.IsValid)
            {
                UsuarioLogado usuarioLogado = new UsuarioLogado
                {
                    UserName = m.Login
                };
                IdentityResult result = await _userManager.CreateAsync(usuarioLogado, m.Senha);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(usuarioLogado, isPersistent: false);
                    if (_medicoDao.CadastrarMedico(m))
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Este e-mail já está sendo utilizado");
                }
                AdicionarErros(result);
            }
            return View(m);
        }

        public void AdicionarErros(IdentityResult result)
        {
            foreach (var erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Medico m)
        {
            var result = await _signInManager.PasswordSignInAsync(m.Login, m.Senha, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Medico");
            }
            ModelState.AddModelError("", "Falha no Login");
            return View();
        }
    }
}