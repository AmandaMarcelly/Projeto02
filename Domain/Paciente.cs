using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("Paciente")]
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do Paciente:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "CPF do Paciente:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Cpf { get; set; }

        [Display(Name = "Idade do Paciente:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Idade { get; set; }

        [Display(Name = "Telefone do Paciente:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Telefone { get; set; }

        [Display(Name = "Login:")]
        public string Login { get; set; }

        [Display(Name = "Senha:")]
        public string Senha { get; set; }

        [Display(Name = "Confirmação da senha:")]
        [NotMapped]
        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        public string ConfirmacaoSenha { get; set; }



    }
}
