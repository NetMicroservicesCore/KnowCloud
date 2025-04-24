using KnowCloud.Contract;
using KnowCloud.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [HttpPost]
        public async Task<IActionResult> Create(CouponDto couponDto)
        {
            if (ModelState.IsValid)
            {
                //creamos el cupon generado por el backoffice del sistema
                ResponseDto response = await _couponService.CreateCouponsAsync(couponDto);
                if (response != null && response.IsSuccess)
                {
                    //si se crea de forma adecuada regresamos a la vista con todos los cupones creados.
                    TempData["success"] = "El cupon fue creado correctamente";
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(couponDto);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CouponDto> coupons = new();
            ResponseDto response = await _couponService.GetAllCouponsAsync();
            if (response != null && response.IsSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
            else 
            {
                TempData["error"] = response?.Message;
            }
            return View(coupons);
        }

    }

}
