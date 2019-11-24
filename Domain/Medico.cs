using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    class Medico
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do Medico:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "CRM do médico:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Crm { get; set; }


        [Display(Name = "Especialidade:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public int Especialidade { get; set; }

      /*[Display(Name = "Categoria:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public Categoria Categoria { get; set; }
        */
    }
}