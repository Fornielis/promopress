using Microsoft.AspNetCore.Identity;
using WEB.Context;
using WEB.Models;
using WEB.Repositories.Interfaces;
using WEB.ViewModels;

namespace WEB.Repositories.Interfaces
{
    public class LoginRepository : ILoginRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public LoginRepository(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public bool Login(UsuarioViewModel ususario)
        {
            var result = _signInManager.PasswordSignInAsync(ususario.Usuario,ususario.Senha, false, false).Result;

            if (result.Succeeded)
                return true;

            return false;
        }
        public void Sair()
        {
            _signInManager.SignOutAsync();
        }
    }
}
