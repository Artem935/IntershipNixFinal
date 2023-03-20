using Microsoft.AspNetCore.Mvc;
using PresentationMVC.Models;
using System.Diagnostics;
using Aplication.Interfaces;
using Domain.Models;
using Data.Context;

using Data.Repository;

namespace PresentationMVC.Controllers
{
    public class HomeController : Controller
    {
        private ShopDbContext _context;

        private readonly ILogger<HomeController> _logger;
        private readonly ICarServices _carService;
        /*        private readonly IUserServices _UserService;*/
        public HomeController(ILogger<HomeController> logger, ICarServices carService, ShopDbContext context)
        {
            _logger = logger;
            _carService = carService;
            _context = context;
        }
        /*        public IActionResult Index()
                {
                    return View(_carService.GetViewCar());
                }*/
        [HttpGet]
        public IActionResult Index(int page,Menu menu)
         {
                int pageSize = 3; // количество объектов на страницу
                if (page < 1)
                    page = 1;
                IEnumerable<Car> cars = _context.Cars;
                cars = new SortingByMenu().Sorting(cars,menu);
                int recsCount = cars.Count();
                var pager = new Pager(recsCount, page, pageSize);
                int recSkip = (page - 1) * pageSize;
                cars = cars.Skip(recSkip).Take(pager.PageSize);
                ViewBag.Pager = pager;
                HomeViewModel ivn = new HomeViewModel(cars,menu);
                return View(ivn);
        }

        public IActionResult Menu(Menu model)
        {
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        
    }
}