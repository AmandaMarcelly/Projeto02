using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("MedicosCRM")]
    public class Item
    {
        public Item()
        {
            CriadoEm = DateTime.Now;
        }
        [Key]
        public int MedicoCrmId { get; set; }

        [Display(Name = "Tipo: ")]
        public string tipo { get; set; }

        [Display(Name = "Nome: ")]
        public string nome { get; set; }

        [Display(Name = "Numero: ")]
        public string numero { get; set; }

        [Display(Name = "Profissão: ")]
        public string profissao { get; set; }

        [Display(Name = "UF: ")]
        public string uf { get; set; }

        [Display(Name = "Situação: ")]
        public string situacao { get; set; }

        public DateTime CriadoEm { get; set; }
    }
}