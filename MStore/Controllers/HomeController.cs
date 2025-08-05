using ECommerceStore.BLL.Interface;
using Microsoft.AspNetCore.Mvc;
using MStore.Models;
using System.Diagnostics;

namespace MStore.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var latest = _productService.GetAllWithCategory()
                .OrderByDescending(p => p.ProductID)                                                        
                .Take(8);
            return View(latest);
        }

        public IActionResult Privacy()
        {
            return View();
        }

       

    }
}
