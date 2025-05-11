using KnowCloud.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KnowCloud.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;


        public IActionResult Index()
        {
            return View();
        }
    }
}
