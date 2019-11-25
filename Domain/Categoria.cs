using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
   public class Categoria
    {
        [Key]
        public int CategoriaId { get; set; }

        [Display(Name = "Nome da Categoria:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "**:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public List<Medico> Medicos { get; set; }



    }
}
