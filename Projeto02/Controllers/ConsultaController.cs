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
            Consulta consulta = new Consulta();
            consulta.Id = 0;

            return View();
        }

        /*

                [HttpPost]
                public async Task<IActionResult> CadastroConsulta(Consulta c)
                {
                    if (ModelState.IsValid)
                    {
                        if (c.Id != 0)
                        {
                            _consultaDao.AlterarConsulta(c);

                            return RedirectToAction("MenuPrincipalPaciente", "Paciente");


                        }
                        else
                        {
                            Consulta
                        }




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
                */







        public IActionResult ListagemConsultas()
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