using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductsApp.Infrastructure;
using ProductsApp.Models;
using System.Diagnostics;

namespace ProductsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductFacade _productFacade;

        public HomeController(ILogger<HomeController> logger,IProductFacade productFacade)
        {
            _logger = logger;
            _productFacade = productFacade;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Index function has been called");

            var productViewModel = await _productFacade.GetProducts();

            return View(productViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}