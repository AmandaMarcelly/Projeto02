using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net;

namespace Projeto02.Controllers
{
    public class MedicoController : Controller
    {
        private readonly MedicoDao _medicoDao;
        private readonly CategoriaDao _categoriaDao;
        private readonly DisponibilidadeDao _disponibilidadeDao;
        private readonly UserManager<UsuarioLogado> _userManager;
        private readonly SignInManager<UsuarioLogado> _signInManager;

        public MedicoController(MedicoDao medicoDao, CategoriaDao categoriaDao, 
            DisponibilidadeDao disponibilidadeDao, 
            UserManager<UsuarioLogado> userManager, 
            SignInManager<UsuarioLogado> signInManager
            )
        {
            _medicoDao = medicoDao;
            _categoriaDao = categoriaDao;
            _disponibilidadeDao = disponibilidadeDao;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult MenuPrincipalMedico()
        {
            return View();
        }

        public IActionResult CadastroMedico()
        {
            ViewBag.Categorias = new SelectList(_categoriaDao.ListarCategoria(), "CategoriaId", "Nome");
            Medico medico = new Medico();

            if (TempData["Crm"] != null)
            {
                string resultado = TempData["Crm"].ToString();
                ResultadoJson medicoResultado = JsonConvert.DeserializeObject<ResultadoJson>(resultado);
                medico.Nome = medicoResultado.item[0].nome;
                medico.Uf = medicoResultado.item[0].uf;
                medico.Crm = medicoResultado.item[0].numero;
            }
            else if (_userManager.GetUserName(User) != null)
            {
                medico = _medicoDao.BuscarMedicoPorLogin(_userManager.GetUserName(User));
                ViewBag.senha = medico.Senha;
            }

            return View(medico);
        }

        [HttpPost]
        public async Task<IActionResult> CadastroMedico(Medico m, int Categoria)
        {
            ViewBag.Categorias = new SelectList(_categoriaDao.ListarCategoria(), "CategoriaId", "Nome");
            m.Categoria = _categoriaDao.BuscarCategoriaPorId(Categoria);

            //if (ModelState.IsValid)
           // {
                UsuarioLogado usuarioLogado = new UsuarioLogado
                {
                    UserName = m.Login,
                    PhoneNumber = m.Senha
                };
                IdentityResult result = await _userManager.CreateAsync(usuarioLogado, m.Senha);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(usuarioLogado, isPersistent: false);
                    if (_medicoDao.CadastrarMedico(m))
                    {
                        return RedirectToAction("MenuPrincipalMedico");
                    }
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "Este e-mail já está sendo utilizado");
                }
                AdicionarErros(result);
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
                return RedirectToAction("MenuPrincipalMedico", "Medico");
            }
            ModelState.AddModelError("", "Falha no Login");
            return View();
        }


        public IActionResult DisponibilidadeMedico()
        {
            Medico _medico = _medicoDao.BuscarMedicoPorLogin(_userManager.GetUserName(User));
            List<Disponibilidade> disponibilidades = _disponibilidadeDao.ListarDisponiblidade();
            Disponibilidade disponibilidade = new Disponibilidade();

            foreach (var dipo in disponibilidades)
            {
                if (dipo.Medico.Id == _medico.Id)
                {
                    disponibilidade = dipo;
                    break;
                }
            }

            return View(disponibilidade);
        }

        [HttpPost]
        public IActionResult DisponibilidadeMedico(Disponibilidade disponibilidade)
        {
            if (ModelState.IsValid)
            {
                disponibilidade.Medico = _medicoDao.BuscarMedicoPorLogin(_userManager.GetUserName(User));
                if (disponibilidade.Id == 0)
                {
                    

                    if (_disponibilidadeDao.CadastrarDisponibilidade(disponibilidade))
                    {
                        return RedirectToAction("MenuPrincipalMedico");
                    }
                    ModelState.AddModelError("", "Algum erro aconteceu");
                }
                if (_disponibilidadeDao.AlterarDisponibilidade(disponibilidade))
                {
                    return RedirectToAction("MenuPrincipalMedico");
                }
                ModelState.AddModelError("", "Algum erro aconteceu");

            }
            return View(disponibilidade);
        }

        [HttpPost]
        public IActionResult BuscarCrmApi(Medico m)
        {
            string url = "https://www.consultacrm.com.br/api/index.php?tipo=crm&uf=" + m.Uf + "&q=" + m.Crm + "&chave=6345776308&destino=json";
            WebClient client = new WebClient();
            TempData["Crm"] = client.DownloadString(url);

            return RedirectToAction(nameof(CadastroMedico));
        }

        public async Task<IActionResult> RemoverMedico()
        {
            Medico medico = _medicoDao.BuscarMedicoPorLogin(_userManager.GetUserName(User));
            await Logout();
            _medicoDao.RemoverMedico(medico);
            return RedirectToAction("Index", "Home");
        }
    }
}