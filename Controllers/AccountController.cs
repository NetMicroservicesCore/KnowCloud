using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace KnowCloud.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Perfil()
        {
            return View();
        }


        [HttpGet]
        public  IActionResult Login() 
        {
            return View();
        }
    }
}
