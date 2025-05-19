using KnowCloud.Models.Dto;
using KnowCloud.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KnowCloud.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            //List<ProductDto>? productos = new();
            ////aqui yo meteria mejor una consulta de traer solo cierta cantidad de elementos para no cargar toda la tienda en linea
            //ResponseDto? response = await _productService.GetAllProductsAsync();
            //if (response != null && response.IsSuccess)
            //{
            //    productos = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            //}
            //else 
            //{
            //    TempData["error"] = response?.Message;
            //}
            return View();
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto productDto)
        {
            if (ModelState.IsValid) 
            {
                ResponseDto? response = await _productService.CreateProductsAsync(productDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "El producto se creo de forma correcta";
                    return RedirectToAction(nameof(Index));
                }
                else 
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(productDto);
        }


        [HttpPut]
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            return View(productId);
        }


        public async Task<IActionResult> All()
        {
            List<ProductDto>? productos = new();
            //aqui yo meteria mejor una consulta de traer solo cierta cantidad de elementos para no cargar toda la tienda en linea
            ResponseDto? response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess)
            {
                productos = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return Json(new { data= productos});
        }

    }
}
