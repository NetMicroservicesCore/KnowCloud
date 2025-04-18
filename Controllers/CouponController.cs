using KnowCloud.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KnowCloud.Controllers
{
    
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        private readonly ILogger<HomeController> _logger;

      
        public CouponController(ICouponService couponService, ILogger<HomeController> logger)
        {
            _couponService = couponService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Create()
        {
            _logger.LogInformation("Estamos ingresando a la vista");
            return View();
        }
    }
}
