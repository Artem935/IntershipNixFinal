using Microsoft.AspNetCore.Mvc;
using PresentationMVC.Models;
using System.Diagnostics;
using Aplication.Interfaces;
using Domain.Models;
using Data.Context;
using Data.Repository;
using System.Linq;

using PresentationMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PresentationMVC.Controllers
{
    public class HomeController : Controller
    {
        

        private readonly ILogger<HomeController> _logger;
        private readonly ICarServices _carService;
        private ShopDbContext _сontext;
        private PresantationMVCDbContext _PresentationContext;
        /*        private readonly IUserServices _UserService;*/
        public HomeController(ILogger<HomeController> logger, ICarServices carService, ShopDbContext Context,
            PresantationMVCDbContext PresentationContext)
        {
            _logger = logger;
            _carService = carService;
            _сontext = Context;
            _PresentationContext = PresentationContext;
        }

        [HttpGet]
        public IActionResult Index(Menu menu, int page, int carID)
        {
            if (carID != 0)
            {

                return RedirectToAction("ProductOverview", new { carID });
            }
            int pageSize = 3; // количество объектов на страницу
            if (page < 1)
                page = 1;
            IEnumerable<Car> cars = _сontext.Cars;
            cars = new FilterByMenu().CarFilter(cars, menu);
            int recsCount = cars.Count();
            var pager = new Pager(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            cars = cars.Skip(recSkip).Take(pager.PageSize);
            ViewBag.Pager = page;
            HomeViewModel ivn = new HomeViewModel(cars, menu, pager);
            return View(ivn);
        }
        public IActionResult Menu(Menu model)
        {
            return View(model);
        }

        public IActionResult ProductOverview(int carID)
        {
            IEnumerable<Car> cars = _сontext.Cars;
            cars = cars.Where(p => p.Id == carID);
            IdentityUser identityUser = new();
            foreach ( var item in cars)
            {
                foreach (var i in _PresentationContext.Users.Where(p => p.Id == item.UserId))
                {
                    identityUser = i;
                }
            }
            User user = new User();
            ProductOwerviewModel pom = new ProductOwerviewModel(cars, user);
            return View(pom);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}