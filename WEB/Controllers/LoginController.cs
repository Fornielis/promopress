using Microsoft.AspNetCore.Mvc;
using WEB.Repositories.Interfaces;
using WEB.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace WEB.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginRepository _loginRepository;
        public LoginController(ILoginRepository iLoginRepository)
        {
            _loginRepository = iLoginRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel(); 
            return View(usuarioViewModel);
        }

        [HttpPost]
        public IActionResult Login(UsuarioViewModel usuarioViewModel)
        {
            bool login = _loginRepository.Login(usuarioViewModel);

            if (login == true)
                return RedirectToAction("Lista", "Arquivo");

            return View("Login", usuarioViewModel);
        }

        [HttpGet]
        public IActionResult Sair()
        {
            _loginRepository.Sair();
            return RedirectToAction("Login", "Login");
        }
    }
}
