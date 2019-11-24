using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    class Consulta
    {
        [Key]
        public int Id { get; set; }

        public DateTime CriadoEm { get; set; }

        public Medico medico { get; set; }

        public Paciente paciente { get; set; }

        [Display(Name = "Anotações:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Anotacao { get; set; }

    }
}
