
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

namespace PresentationMVC.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private PresantationMVCDbContext _context;



        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
            ILogger<AccountController> logger, PresantationMVCDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(Models.User user)
        {
           
            if (ModelState.IsValid)
            {
                var user1 = new IdentityUser { UserName = user.UserName, Email = user.Email,PhoneNumber = user.PhoneNumber };
                var result = await _userManager.CreateAsync(user1, user.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Login", "Account");
                }
                HttpContext.Session.SetString("UserId", user.Id);
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
            _context.SaveChanges();
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, true, false);
                if (result.Succeeded)
                {
                    IdentityUser user = new IdentityUser();
                    foreach (var item in _context.Users.Where(p => p.UserName == login.UserName)) 
                    {
                        user = item;
                    }

                    /*string s = _userManager.GetUserId(HttpContext.User);*/

                    HttpContext.Session.SetString("UserId", user.Id);

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
            IdentityUser user = new IdentityUser();
            foreach (var item in _context.Users.Where(p => p.Id == HttpContext.Session.GetString("UserId")))
            {
                user = item;
            }
            
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(IdentityUser identity)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");
            IdentityUser user = new();
            foreach (var item in _context.Users.Where(p => p.Id == HttpContext.Session.GetString("UserId")))
            {
                user = item;
            }
            user.UserName= identity.UserName;
            user.Email= identity.Email;
            user.PhoneNumber= identity.PhoneNumber;

            /*_userManager.UpdateAsync(user);*/
            _context.Users.Update(user);
            _context.SaveChanges();
            return View(user);
        }

    }
}
