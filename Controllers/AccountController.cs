using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _4zadanie.Models;
using Microsoft.AspNetCore.Identity;
using _4zadanie.ViewModels;
using System;
using _4zadanie.Services;

namespace _4zadanie.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSignInManager _AppSignInManager;

        public AccountController(UserManager<User> userManager, AppSignInManager signInManager,
            ApplicationContext context)
        {
            _userManager = userManager;
            _AppSignInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Email = model.Email,
                    UserName = model.Name,
                    RegistrationDate = DateTimeOffset.UtcNow.LocalDateTime,
                    LastLoginTime = DateTimeOffset.UtcNow.LocalDateTime,
                    IsEnabled = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _AppSignInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _AppSignInManager.PasswordSignInAsync(model.Name, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        User user = await _userManager.FindByNameAsync(model.Name);

                        user.LastLoginTime = DateTimeOffset.Now;

                        await _userManager.UpdateAsync(user);
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect user name and (or) password");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _AppSignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
