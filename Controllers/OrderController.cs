using Microsoft.AspNetCore.Mvc;

namespace KnowCloud.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
