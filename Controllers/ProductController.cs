using KnowCloud.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace KnowCloud.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
    }
}
