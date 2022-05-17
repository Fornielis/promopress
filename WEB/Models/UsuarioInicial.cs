using Microsoft.AspNetCore.Identity;

namespace WEB.Models
{
    public class UsuarioInicial
    {
        public string UserName { get; set; }
        public string Senha { get; set; }
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public string RoleName { get; set; }
        public string RoleNormalizedName { get; set; }
    }
}
