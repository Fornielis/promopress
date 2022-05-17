using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}
