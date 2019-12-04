using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;
using System;

namespace Projeto02.Controllers
{
    public class PacienteController : Controller
    { 
        private readonly PacienteDao _pacienteDAO;
        private readonly UserManager<UsuarioLogado> _userManager;
        private readonly SignInManager<UsuarioLogado> _signInManager;
        

        public PacienteController(PacienteDao pacienteDao
            , UserManager<UsuarioLogado> userManager
            , SignInManager<UsuarioLogado> signInManager
            )
        {
            _pacienteDAO = pacienteDao;
            _userManager = userManager;
            _signInManager = signInManager;
        }
    
        public IActionResult MenuPrincipalPaciente()
        {
            return View();
        }

        public IActionResult CadastroPaciente()
        {
            Paciente paciente = new Paciente();
            paciente.Id = 0;
            if (_userManager.GetUserName(User) != null)
            {
                paciente = _pacienteDAO.BuscarPacientePorLogin(_userManager.GetUserName(User));
                ViewBag.senha = paciente.Senha;
            }
            
            return View(paciente);
        }

        [HttpPost]
        public async Task<IActionResult> CadastroPaciente(Paciente p)
        {
            if (ModelState.IsValid)
            {
                if (p.Id != 0)
                {
                    _pacienteDAO.AlterarPaciente(p);
                    return RedirectToAction("MenuPrincipalPaciente", "Paciente");
                }
                else
                {
                    UsuarioLogado usuarioLogado = new UsuarioLogado
                    {
                        UserName = p.Login,
                        PhoneNumber = p.Senha
                    };
                    IdentityResult result = await _userManager.CreateAsync(usuarioLogado, p.Senha);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(usuarioLogado, isPersistent: false);
                        if (_pacienteDAO.CadastrarPaciente(p))
                        {
                            return RedirectToAction("MenuPrincipalPaciente");
                        }
                        await _signInManager.SignOutAsync();
                        ModelState.AddModelError("", "Este login já está sendo utilizado");
                    }
                    AdicionarErros(result);
                }
            }
            return View(p);
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
        public async Task<IActionResult> Login(Paciente p)
        {
            var result = await _signInManager.PasswordSignInAsync(p.Login, p.Senha, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return RedirectToAction("MenuPrincipalPaciente", "Paciente");
            }
            ModelState.AddModelError("", "Falha no Login");
            return View();
        }
        
        public async Task<IActionResult> RemoverPaciente()
        {
            Paciente paciente = _pacienteDAO.BuscarPacientePorLogin(_userManager.GetUserName(User));
            await Logout();
            _pacienteDAO.RemoverPaciente(paciente);
            return RedirectToAction("Index", "Home");
        }
    }
}