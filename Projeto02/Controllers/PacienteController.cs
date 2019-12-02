using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;

namespace Projeto02.Controllers
{
    public class PacienteController : Controller
    { 
        private readonly PacienteDao _pacienteDAO;
        //private readonly UserManager<UsuarioLogado> _userManager;
        //private readonly SignInManager<UsuarioLogado> _signInManager;

        public PacienteController(PacienteDao pacienteDao
            //, UserManager<UsuarioLogado> userManager, SignInManager<UsuarioLogado> signInManager
            )
        {
            _pacienteDAO = pacienteDao;
          //  _userManager = userManager;
            //_signInManager = signInManager;
        }


        public IActionResult MenuPrincipalPaciente()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastroPaciente()
        {
            Paciente paciente = new Paciente();
            
            return View(paciente);
        }

        [HttpPost]
        public IActionResult CadastroPaciente(Paciente p)
        {
            //if (ModelState.IsValid)
            //{
                UsuarioLogado usuarioLogado = new UsuarioLogado
                {
                    UserName = p.Login
                };
                //IdentityResult result = await _userManager.CreateAsync(usuarioLogado, p.Senha);
                //if (result.Succeeded)
                //{
                  //  await _signInManager.SignInAsync(usuarioLogado, isPersistent: false);
                    if (_pacienteDAO.CadastrarPaciente(p))
                    {
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", "Este e-mail já está sendo utilizado");
                //}
                //AdicionarErros(result);
            //}
            return View(p);
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
        public async Task<IActionResult> Login(Paciente p)
        {
            //var result = await _signInManager.PasswordSignInAsync(p.Login, p.Senha, true, lockoutOnFailure: false);
            //if (result.Succeeded)
            //{
                return RedirectToAction("Index", "Paciente");
            //}
            //ModelState.AddModelError("", "Falha no Login");
            //return View();
        }

    }
}