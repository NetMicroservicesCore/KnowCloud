﻿using KnowCloud.Models.Dto;
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
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderDto> listado;
            string userId = string.Empty;
            //obtenemos los roles del usuario que vienen junto con los datos del token
            if (!User.IsInRole(Utility.Utilities.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sid)?.FirstOrDefault()?.Value;
            }
            //si el usuario es admin, no necesitamos el userId
            ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                listado = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
                switch (status) 
                {
                    case "approved":
                        listado = listado.Where(u=> u.Status == Utility.Utilities.Status_Approved);
                        break;
                    case "readyforpickup":
                        listado = listado.Where(u => u.Status == Utility.Utilities.Status_ReadyForPickup);
                        break;
                    case "cancelled":
                        listado = listado.Where(u => u.Status == Utility.Utilities.Status_Cancelled);
                        break;
                    default:
                        break;

                }
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

            //obtenemos el orderHeaderDto
            var response = await _orderService.GetOrder(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }
            //validamos el rol del usuario.
            if (!User.IsInRole(Utility.Utilities.RoleAdmin) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }
            return View(orderHeaderDto);
        }


        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId,Utility.Utilities.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "El status fue actualizado exitosamente";
                return RedirectToAction(nameof( OrderDetail),new { orderId = orderId});
            }
            return View();
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, Utility.Utilities.Status_Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "El status fue actualizado exitosamente";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

    }
}
