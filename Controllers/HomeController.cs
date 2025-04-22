using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KnowCloud.Models;
using Microsoft.AspNetCore.Authorization;
using KnowCloud.Services.Contract;
using KnowCloud.Models.Dto;
using Newtonsoft.Json;

namespace KnowCloud.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    private readonly ICartService _cartService;
         
    public HomeController(ICartService cartService, IProductService productService, ILogger<HomeController> logger)
    {
        _productService = productService;
        _cartService = cartService;
        _logger = logger;

    }

    public async Task<IActionResult> Index()
    {
        List<ProductDto> list = new List<ProductDto>();

        ResponseDto response = await _productService.GetAllProductsAsync();

        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(list);
    }

    [Authorize(Policy = "AdminPolicy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }




    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ProductDetails(int productId)
    {
        ProductDto model = new();

        ResponseDto response = await _productService.GetProductByIdAsync(productId);

        if (response != null && response.IsSuccess)
        {
            model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
        }
        else
        {
            TempData["error"] = response?.Message;
        }

        return View(model);
    }

    [Authorize]
    [HttpPost]
    [ActionName("ProductDetails")]
    public async Task<IActionResult> ProductDetails(ProductDto productDto) 
    {
        ProductDto productDto = new();
        //consumimos el microservicio consultando el microservicio por identificador  
        ResponseDto response = await _productService.GetProductByIdAsync(productId);
        if (Response != null && response.IsSuccess)
        {
            //deserializamos el objeto del microservicio
            productDto = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
        }
        else {
            TempData["error"] = response?.Message;
        }
        return View(productDto);
    }

}
