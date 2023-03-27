
using PresentationMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationMVC.Models;
using System.Collections;
using Microsoft.AspNetCore.Http;
using Data.Context;
using PresentationMVC.Data;
using System.Security.Claims;
using Domain.Models;
using Aplication.Intarfaces;

namespace PresentationMVC.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private PresantationMVCDbContext _presentationContext;
        private IUserServices _userServices;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger, PresantationMVCDbContext presentationContext, IUserServices userServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _presentationContext = presentationContext;
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(User user)
        {
            if (ModelState.IsValid)
            {
                    var user1 = new IdentityUser { UserName = user.UserName, Email = user.Email, PhoneNumber = user.PhoneNumber };
                    var result = await _userManager.CreateAsync(user1, user.Password);
                    if (result.Succeeded)
                    {
                        user.UserId = _presentationContext.Users.SingleOrDefault(p => p.UserName == user.UserName).Id;
                        user.Password = "";
                        user.ConfirmPassword = "";
                        _userServices.Add(user);
                        return RedirectToAction("Login", "Account");
                    }
            }
            foreach (var item in ModelState)
            {
                foreach (var a in item.Value.Errors)
                {
                    _logger.LogError(a.ErrorMessage);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true, false);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("UserId", _presentationContext.Users.FirstOrDefault(p => p.UserName == login.UserName).Id);
                    return RedirectToAction("index", "Home");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string s = HttpContext.Session.GetString("UserId");
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            return View(_presentationContext.Users.FirstOrDefault(p => p.Id == HttpContext.Session.GetString("UserId")));
        }
        [HttpPost]
        public async Task<IActionResult> Profile(IdentityUser identity)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            var user = _presentationContext.Users.FirstOrDefault(p => p.Id == HttpContext.Session.GetString("UserId"));
            user.UserName= identity.UserName;
            user.Email= identity.Email;
            user.PhoneNumber= identity.PhoneNumber;

            _userManager.UpdateAsync(user);
            _presentationContext.SaveChanges();
            return View(user);
        }

    }
}
