using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Aplication.Interfaces;
using Aplication.Services;
using Data.Context;
using PresentationMVC.Models;
using Data.Repository;

namespace PresentationMVC.Controllers
{
    public class ProductController : Controller
    {
        private ShopDbContext _context;
        private readonly ICarServices _carService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger, ICarServices carService, ShopDbContext context)
        {
            _logger = logger;
            _carService = carService;
            _context = context;
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCar(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.Add(car);
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
        public IActionResult YourAds(int page, Menu menu)
        {
            int pageSize = 3; // количество объектов на страницу
            if (page < 1)
                page = 1;
            IEnumerable<Car> cars = _context.Cars;
            cars = new SortingByMenu().Sorting(cars, menu);
            int recsCount = cars.Count();
            var pager = new Pager(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            cars = cars.Skip(recSkip).Take(pager.PageSize);
            ViewBag.Pager = pager;
            HomeViewModel ivn = new HomeViewModel(cars, menu);
            return View(ivn);
        }

    }
    
}
