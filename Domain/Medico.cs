using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Medico
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome do Medico:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Nome { get; set; }

        [Display(Name = "CRM do médico:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Crm { get; set; }
        
        [Display(Name = "Categoria:")]
        public Categoria Categoria { get; set; }

        [Display(Name = "Uf:")]
        [Required(ErrorMessage = "Campo obrigatório!")]
        public string Uf { get; set; }

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