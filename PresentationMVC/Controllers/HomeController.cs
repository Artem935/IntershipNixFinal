 using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using PresentationMVC.Models;
using System.Diagnostics;
using Aplication.Interfaces;
using Domain.Models;

namespace PresentationMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarServices _carService;
/*        private readonly IUserServices _UserService;*/
        public HomeController(ILogger<HomeController> logger, ICarServices carService)
        {
            _logger = logger;
            _carService = carService;
        }
        public IActionResult Index()
        {
            return View(_carService.GetViewCar());
        }
        public IActionResult Product()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}