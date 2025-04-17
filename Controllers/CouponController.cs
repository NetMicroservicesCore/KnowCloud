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

        //public CouponController()
        //{
                
        //}
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
    }
}
