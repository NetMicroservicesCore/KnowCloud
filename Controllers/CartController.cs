using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace KnowCloud.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        public CartController(ICartService cartService, IOrderService orderService)
        {
            _cartService = cartService;
            _orderService = orderService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartBaseOnLoggedInUser());
        }

        #region  Orden de Compra de la tienda en Linea
        
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartBaseOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("Checkout")] 
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            CartDto cart = await LoadCartBaseOnLoggedInUser();
            cart.CartHeader.Phone = cartDto.CartHeader.Phone;
            cart.CartHeader.Email = cartDto.CartHeader.Email;
            cart.CartHeader.Name = cartDto.CartHeader.Name;
            //actualizamoso creamos la orden del servicio
            var response = await _orderService.CreateOrder(cart);
            OrderHeaderDto orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            //PREPARAMOS TODO PARA HACER EL PAGO EN LINEA
            if (response != null && response.IsSuccess)
            {
                //hacemos pasarela de pagos y redireccionamos el stripe  al lugar de la orden de trabajo
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                StripeRequestDto stripeRequestDto = new()
                {
                    ApprovedUrl = domain + "Cart/Confirmation?orderId=" + orderHeaderDto.OrderHeaderId,
                    CancelUrl = domain + "Cart/Checkout",
                    OrderHeaderDto = orderHeaderDto
                };
                var stripeResponse = await _orderService.CreateStripeSession(stripeRequestDto);
                StripeRequestDto stripeResponseResult = JsonConvert.DeserializeObject<StripeRequestDto>(Convert.ToString(stripeResponse.Result));
                //establecemos hacia donde vamos a redireccionar 
                //nos redirege a la session y url adecuada
                Response.Headers.Add("Location",stripeResponseResult.StripeSessionUrl);
                return new StatusCodeResult(303);//este codigo establece que nos va a redireccionar a otro recurso
            }
            return View();
        }
        
        #endregion

        #region Confirmacion de Pago

        [Authorize]
        public async Task<IActionResult> Confirmation(int orderId)
        {
            ResponseDto responseDto = await _orderService.ValidateStripeSession(orderId);
            if (responseDto != null && responseDto.IsSuccess)
            {
                //como la url de stripe devuelve el status de intento de pago debemos deserialziar el objeto que envia stripe a traves de nuestra api
                OrderHeaderDto orderHeader = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(responseDto.Result));
                if (orderHeader.Status == Utility.Utilities.Status_Approved)
                {
                    return View(orderId);
                }
                
            }
            //redirigimos alguna pagina de error basado en el status de stripe.
           return View(orderId);

        }
        #endregion


        public async Task<IActionResult> Remove(int cartDetailsId) 
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto response = await _cartService.RemoveFromCartAsync(cartDetailsId);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "El carrito se actualizo correctamente";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RemoveCoupon(CartDto cartDto)
        {
            cartDto.CartHeader.CouponCode = "";
            ResponseDto response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null & response.IsSuccess)
            {
                TempData["success"]="El carrito se actualizo correctamente";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }
        public async Task<IActionResult> AplyCoupon(CartDto cartDto)
        {
            
            ResponseDto response = await _cartService.ApplyCouponAsync(cartDto);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "El carrito se actualizo correctamente";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }


        /// <summary>
        /// este metodo se encarga de consultar todos los elementos de un carrito de compras de un 
        /// usuario logeado al sistema
        /// </summary>
        /// <returns>una tarea con la entidad del tipo cartDto</returns>
        private async Task<CartDto> LoadCartBaseOnLoggedInUser()
        {
            var userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            ResponseDto response = await _cartService.GetCartByUserIdAsnyc(userId);
            if (response != null & response.IsSuccess && response.Result != null)
            {
                string jsonResult = JsonConvert.SerializeObject(response.Result);
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(jsonResult); 
                return cartDto;
            }
            return new CartDto();

        }


        [HttpPost]
        public async Task<IActionResult> EmailCart(CartDto cartDto)
        {
            ResponseDto? response = await _cartService.EmailCart(cartDto);
            if (response != null & response.IsSuccess)
            {
                TempData["success"] = "El carrito fue actualizado correctamente";
                return RedirectToAction(nameof(CartIndex));
            }
            return View();
        }


    }
}

