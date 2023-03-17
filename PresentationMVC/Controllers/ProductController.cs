using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Aplication.Interfaces;
using Aplication.Services;

namespace PresentationMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICarServices _carService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger, ICarServices carService)
        {
            _logger = logger;
            _carService = carService;
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCar(Car model)
        {

            if (ModelState.IsValid)
            {
                _carService.Add(model);
            }
            else
            {
                foreach (var item in ModelState)
                {
                    foreach (var a in item.Value.Errors)
                    {
                        _logger.LogError(a.ErrorMessage);
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Test(Car model)
        {

            if (ModelState.IsValid)
            {
                _carService.Add(model);
            }
            else
            {
                foreach (var item in ModelState)
                {
                    foreach (var a in item.Value.Errors)
                    {
                        _logger.LogError(a.ErrorMessage);
                    }
                }
            }
            return View();
        }
    }
    
}
