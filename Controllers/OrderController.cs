using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace KnowCloud.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<OrderHeaderDto> listado;
            string userId = string.Empty;
            if (!User.IsInRole(Utility.Utilities.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            }
            ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                listado = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
            }
            else 
            {
                listado = new List<OrderHeaderDto>();
            }
            return Json(new { data = listado });
        }


        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            //utilizamos el null condicional
            string userId = User.Claims.Where(u=>u.Type==JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            var response = await _orderService.GetOrder(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }
            if (!User.IsInRole(Utility.Utilities.RoleAdmin) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }
            return View(orderHeaderDto);
        }


        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId,Utility.Utilities.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "El status fue actualizado exitosamente";
                return RedirectToAction(nameof( OrderDetail),new { orderId = orderId});
            }
            return View();
        }

        [HttpPost("OrderReadyForPickup")]
        public async Task<IActionResult> OrderReadyForPickup(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, Utility.Utilities.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "El status fue actualizado exitosamente";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

    }
}
