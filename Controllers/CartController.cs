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
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> CartIndex()
        {
            return View(await LoadCartBaseOnLoggedInUser());
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            return View(await LoadCartBaseOnLoggedInUser());
        }

        [HttpPost]
        [ActionName("Checkout")]
        public async Task<IActionResult> Checkout(CartDto cartDto)
        {
            return View(await LoadCartBaseOnLoggedInUser());
        }

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


    }
}
