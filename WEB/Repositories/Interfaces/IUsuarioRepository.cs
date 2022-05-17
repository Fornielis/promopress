using Microsoft.AspNetCore.Identity;
using WEB.ViewModels;

namespace WEB.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        void RoleInicial();
        void UsuarioInicial();
        void Novo(UsuarioViewModel usuario);
        IEnumerable<IdentityUser> usuarios();
        void NovaRole(UsuarioViewModel usuario);
        IEnumerable<IdentityRole> listaRoles();
    }
}
