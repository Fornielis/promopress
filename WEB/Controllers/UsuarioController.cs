using Microsoft.AspNetCore.Mvc;
using WEB.Repositories.Interfaces;
using WEB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WEB.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        
        [HttpGet]
        [Authorize(Roles = "adm,mst")]
        public IActionResult Usuarios()
        {
            UsuarioViewModel usuario = new UsuarioViewModel
            {
                listaUsuario = _usuarioRepository.usuarios()
            };
            return View(usuario);
        }

        [HttpPost]
        [Authorize(Roles = "adm,mst")]
        public IActionResult Usuarios(UsuarioViewModel usuarioViewModel)
        {
            _usuarioRepository.Novo(usuarioViewModel);
            return RedirectToAction("Usuarios", "Usuario");
        }
    }
}
