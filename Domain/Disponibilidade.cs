using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Disponibilidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Medico Medico { get; set; }

        [Display(Name = "Segunda:")]
        public Boolean Segunda { get; set; }

        [Display(Name = "Terça:")]
        public Boolean Terca { get; set; }

        [Display(Name = "Quarta:")]
        public Boolean Quarta { get; set; }

        [Display(Name = "Quinta:")]
        public Boolean Quinta { get; set; }

        [Display(Name = "Sexta:")]
        public Boolean Sexta { get; set; }

        [Display(Name = "Sábado:")]
        public Boolean Sabado { get; set; }

    }
}
