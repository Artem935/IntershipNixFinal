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
        private ShopDbContext _shopContext;
        private PresantationMVCDbContext _presentationContext;
        /*private readonly IUserServices _UserService;*/
        public HomeController(ILogger<HomeController> logger, ICarServices carService, ShopDbContext Context,
            PresantationMVCDbContext PresentationContext)
        {
            _logger = logger;
            _carService = carService;
            _shopContext = Context;
            _presentationContext = PresentationContext;
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
            IEnumerable<Car> cars = _shopContext.Cars;
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
            Car cars = _shopContext.Cars.FirstOrDefault(p => p.Id == carID);
            IdentityUser identityUser = _presentationContext.Users.FirstOrDefault(p => p.Id == cars.UserId);
            User user = _shopContext.ShopUsers.FirstOrDefault(p => p.UserId == identityUser.Id);
            ProductOwerviewModel pom = new ProductOwerviewModel(cars, user);  
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("ProductOverviewNoLogin", new { carID });
            return View(pom);
        }
        public IActionResult ProductOverviewNoLogin(int carID, string call)
        {
            if (call == null)
            {
                Car cars = _shopContext.Cars.FirstOrDefault(p => p.Id == carID);
                IdentityUser identityUser = _presentationContext.Users.FirstOrDefault(p => p.Id == cars.UserId);
                User user = _shopContext.ShopUsers.FirstOrDefault(p => p.UserId == identityUser.Id);
                ProductOwerviewModel pom = new ProductOwerviewModel(cars, user);
                return View(pom);
            }
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}