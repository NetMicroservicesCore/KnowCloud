using Microsoft.AspNetCore.Mvc;

namespace KnowCloud.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Perfil()
        {
            return View();
        }
    }
}
