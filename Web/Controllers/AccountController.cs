using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamHolidayPlanner.Domain;
using TeamHolidayPlanner.Web.Models;
using Microsoft.AspNetCore.Authentication;

namespace TeamHolidayPlanner.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, 
            SignInManager<User> signInManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public IActionResult Index()
        {
            var user = User.Identity as ClaimsIdentity;
            var claims = user.Claims;
            var userIsAuthenticated = user.IsAuthenticated;
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            //signInManager.CheckPasswordSignInAsync()
            if (signInResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            
            ViewData["Error"] = "Invalid login attempt.";

            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                PasswordHash = string.Empty
            };

            IdentityResult identityResult = await userManager.CreateAsync(user, model.Password);

            if(!CheckCreateSuccess(identityResult))
            {
                return View();
            }

            await signInManager.SignInAsync(user, false);

            return RedirectToRoute("/Employees");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // See: https://github.com/aspnet/Security/issues/1310
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

            return RedirectToAction("Index", "Home");
        }

        public bool CheckCreateSuccess(IdentityResult identityResult)
        {
            if (identityResult == IdentityResult.Success)
            {
                return true;
            }

            StringBuilder errorMessageBuilder = new StringBuilder();
            foreach (var error in identityResult.Errors)
            {
                errorMessageBuilder.Append($"{error.Description}");
            }
            ViewData["LoginResponse"] = errorMessageBuilder.ToString();

            return false;
        }
        
    }
}