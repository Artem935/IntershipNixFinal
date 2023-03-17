
using Aplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationMVC.Models;

namespace PresentationMVC.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;



        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("index", "Home");
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.Succeeded)
                    return RedirectToAction("index", "Home");
                _logger.LogError("Username or Password are wrong");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View();
        }


    }
}
