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
using System.Collections.Generic;

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
        public IActionResult Index(Menu menu, int page, int carID, int like)
        {
            int pageSize = 3; // количество объектов на страницу
            if (carID != 0)
                return RedirectToAction("ProductOverview", new { carID });
            if (page < 1)
                page = 1;

            if (like != 0)
            {
                if (HttpContext.Session.GetString("UserId") == null)
                    return RedirectToAction("Login", "Account");

                Liked liked = new Liked();
                liked.CarId = like;
                liked.UserId = HttpContext.Session.GetString("UserId");
                if (_shopContext.LikedCar.Count() != 0)
                {
                    int? LikedCarID;
                    try
                    {
                        LikedCarID = _shopContext.LikedCar.FirstOrDefault(p => p.UserId == HttpContext.Session.GetString("UserId")).CarId;
                    }
                    catch (Exception e)
                    {
                        LikedCarID = 0;
                    }
                    if (LikedCarID != like)
                    {
                        _shopContext.LikedCar.Add(liked);
                        _shopContext.SaveChanges();
                    }
                }
            }

            IEnumerable<Car> cars = _shopContext.Cars;
            cars = new FilterByMenu().CarFilter(cars, menu);
            int recsCount = cars.Count();
            var pager = new Pager(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            cars = cars.Skip(recSkip).Take(pager.PageSize);
            ViewBag.Pager = page;
            IEnumerable <Picture> picture =  _shopContext.Picture;
            HomeViewModel ivn = new HomeViewModel(cars, menu, pager, picture);
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
            User user = _shopContext.Users.FirstOrDefault(p => p.UserId == identityUser.Id);
            int countProducts = _shopContext.Cars.Where(p => p.UserId == identityUser.Id).Count();
            IEnumerable<Picture> picture = _shopContext.Picture;
            ProductOwerviewModel pom = new ProductOwerviewModel(cars, user, countProducts, picture);
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
                User user = _shopContext.Users.FirstOrDefault(p => p.UserId == identityUser.Id);
                int countProducts = _shopContext.Cars.Where(p => p.UserId == identityUser.Id).Count();
                IEnumerable<Picture> picture = _shopContext.Picture;
                ProductOwerviewModel pom = new ProductOwerviewModel(cars, user, countProducts, picture);
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