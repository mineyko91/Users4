using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _4zadanie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using _4zadanie.ViewModels;
using System.Security.Claims;

namespace _4zadanie.Controllers
{

    public class HomeController : Controller
    {
        UserManager<User> _userManager;

        ApplicationContext _context;

        AccountController _accountController;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager,
            ApplicationContext context, AccountController accountController)
        {
            _userManager = userManager;
            _context = context;
            _accountController = accountController;
        }

        [Authorize]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            List<EditUserViewModel> list = new List<EditUserViewModel>();

            foreach (var item in users)
            {
                list.Add(new EditUserViewModel
                {
                    Name = item.UserName,
                    Email = item.Email,
                    Id = item.Id,
                    Selected = false,
                    IsEnabled = item.IsEnabled,
                    LastLoginTime = item.LastLoginTime,
                    RegistrationDate = item.RegistrationDate
                });
            }

            return View(list);
        }

        [HttpPost]
        public async Task<ActionResult> EditUsers(List<EditUserViewModel> users, string submit)
        {
            UsersSelectionViewModel model = new UsersSelectionViewModel() { Users = users };

            var models = model.getSelectedIds();

            foreach (var id in models)
            {
                User user = await _userManager.FindByIdAsync(id.ToString());

                if (user != null)
                {
                    switch (submit)
                    {
                        case "Delete":
                            await _userManager.DeleteAsync(user);
                            break;
                        case "Block":
                            user.IsEnabled = false;
                            await _userManager.UpdateAsync(user); break;
                        case "Unblock":
                            user.IsEnabled = true;
                            await _userManager.UpdateAsync(user); break;
                        default:
                            break;
                    }
                }

                if (submit == "Delete" || submit == "Block")
                {
                    var currentUserName = _context._httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

                    if (user.UserName == currentUserName)
                    {
                        await _accountController.Logout();
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
