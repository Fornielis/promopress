using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using WEB.Context;
using WEB.Models;
using WEB.Repositories.Interfaces;
using WEB.ViewModels;

namespace WEB.Repositories   
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UsuarioInicial _usuarioInicial;

        public UsuarioRepository(
            AppDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<UsuarioInicial> usuarioInicial)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _usuarioInicial = usuarioInicial.Value;
        }

        public void RoleInicial()
        {
            if (!_roleManager.RoleExistsAsync("Master").Result)
            {
                string[] roles = {"MST", "ADM", "USU", "COM"};

                foreach (var item in roles)
                {
                    IdentityRole role = new IdentityRole
                    {
                        Name = item.ToLower(),
                        NormalizedName = item.ToUpper()
                    };

                    _roleManager.CreateAsync(role).Wait();
                }
            }
        }
        public void UsuarioInicial()
        {
            if (_userManager.FindByEmailAsync(_usuarioInicial.Email).Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = _usuarioInicial.UserName,
                    NormalizedUserName = _usuarioInicial.NormalizedUserName,
                    Email = _usuarioInicial.Email,  
                    NormalizedEmail = _usuarioInicial.NormalizedEmail,
                    EmailConfirmed = _usuarioInicial.EmailConfirmed,    
                    LockoutEnabled = _usuarioInicial.LockoutEnabled,    
                    SecurityStamp = Guid.NewGuid().ToString()
            };

                _userManager.CreateAsync(user, _usuarioInicial.Senha);
                _userManager.AddToRoleAsync(user, _usuarioInicial.RoleName);
            }
        }
        public void Novo(UsuarioViewModel usuario)
        {
            IdentityUser user = new IdentityUser {
                UserName = usuario.Usuario,
                Email = usuario.Email
            };

            _userManager.CreateAsync(user, usuario.Senha).Wait();
            _userManager.AddToRoleAsync(user, usuario.Role).Wait();
        }
        public IEnumerable<IdentityUser> usuarios()
        {
            IEnumerable<IdentityUser> users = _userManager.Users.ToList();
            return users;
        }
        public IEnumerable<IdentityRole> listaRoles()
        {
            IEnumerable<IdentityRole> roles = _roleManager.Roles.ToList();
            return roles;
        }
        public void NovaRole(UsuarioViewModel usuario)
        {
            IdentityRole role = new IdentityRole
            {
                Name = usuario.Role,
                NormalizedName = usuario.Role.ToUpper()
            };

            _roleManager.CreateAsync(role).Wait();
        }

    }
}
