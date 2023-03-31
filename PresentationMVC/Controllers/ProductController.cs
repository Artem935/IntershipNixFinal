using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Aplication.Interfaces;
using Aplication.Intarfaces;
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
        private readonly IPictureServices _pictureService;
        public ProductController(ILogger<ProductController> logger, ICarServices carService, ShopDbContext context,
            IPictureServices pictureService)
        {
            _logger = logger;
            _carService = carService;
            _context = context;
            _pictureService = pictureService;
        }

        [HttpGet]
        public IActionResult AddCar()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult AddCar(Car car, IFormFile TakeImage)
        {
            string s = HttpContext.Session.GetString("UserId");
            car.UserId = s;
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(TakeImage.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)TakeImage.Length);
            }
            Picture picture = new();
            picture.Image = imageData;
            if (ModelState.IsValid)
            {
                _carService.Add(car);
                picture.CarId = car.Id;
                _pictureService.Add(picture);
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
            return Redirect("YourAds");
        }
        public IActionResult YourAds(Menu menu, int page,int carDelite, int carEdit)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");
            string UserId = HttpContext.Session.GetString("UserId");
            int pageSize = 3; // количество объектов на страницу
            if (page < 1)
                page = 1;
             IEnumerable<Car> cars = _context.Cars;
            cars = new FilterByMenu().CarFilter(cars, menu);
            int recsCount = cars.Count();
            var pager = new Pager(recsCount, page, pageSize);
            int recSkip = (page - 1) * pageSize;
            cars = cars.Skip(recSkip).Take(pager.PageSize).Where(p => p.UserId == UserId);
            IEnumerable<Picture> pictures = _context.Picture;
                    /*_shopContext.CarPicture;*/
            HomeViewModel ivn = new HomeViewModel(cars, menu, pager, pictures);
            if (carDelite != 0)
            {
                Car car = new Car();
                foreach (var item in _context.Cars.Where(p => p.Id == carDelite))
                    car = item;
                _carService.Remove(car);
            }
            else if (carEdit != 0)
            {
                Car car = new Car();
                foreach (var item in _context.Cars.Where(p => p.Id == carEdit))
                    car = item;
                return RedirectToAction("EditAds", car);
            }
            return View(ivn);
        }
        public IActionResult EditAds(Car car, string save, IFormFile TakeImage)
        {
            if (save == "Save Edits") 
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(TakeImage.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)TakeImage.Length);
                }
                if (ModelState.IsValid)
                {
                    Picture picture = _context.Picture.FirstOrDefault(p => p.CarId == car.Id);
                    picture.Image = imageData;
                    _pictureService.Overwriting(picture);
                    _carService.Overwriting(car);
                    car.UserId = HttpContext.Session.GetString("UserId");
                    return Redirect("../YourAds");
                }
            }
            return View();
        }
        public IActionResult LikedAds(Menu menu, int page, int carDelite)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");
            string UserId = HttpContext.Session.GetString("UserId");
            int pageSize = 3; // количество объектов на страницу
            if (page < 1)
                page = 1;
            IEnumerable<Liked> liked = _context.LikedCar.Where(p => p.UserId == UserId);
            IEnumerable<Car> cars = _context.Cars;

            cars = new FilterByMenu().CarFilter(cars, menu);
            var pager = new Pager(cars.Count(), page, pageSize);
            cars = cars.Skip((page - 1) * pageSize).Take(pager.PageSize);
            IEnumerable<Picture> pictures = _context.Picture;
            HomeViewModel ivn = new HomeViewModel(cars, menu, pager, pictures);
            if (carDelite != 0)
            {
                Liked temp = new();
                foreach (var item in _context.LikedCar.Where(p => p.CarId == carDelite))
                    temp = item;
                _context.LikedCar.Remove(temp);
                _context.SaveChanges();
            }
            ViewBag.Model = liked;
            return View(ivn);
        }
    }

}
