using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WEB.ViewModels
{
    public class UsuarioViewModel
    {
        [Required(ErrorMessage = "Informe Usuário")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Informe E-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a Senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Informe Role")]
        [Display(Name = "Role")]
        public string Role { get; set; }
        public IEnumerable<IdentityUser> listaUsuario { get; set; }
    }
}
