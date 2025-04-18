using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KnowCloud.Models;
using Microsoft.AspNetCore.Authorization;
using KnowCloud.Services.Contract;

namespace KnowCloud.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductService _productService;
    public HomeController(IProductService productService, ILogger<HomeController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
}
