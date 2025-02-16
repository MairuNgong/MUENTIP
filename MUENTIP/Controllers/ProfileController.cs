using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MUENTIP.Models;

namespace MUENTIP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;

        public ProfileController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
                ViewBag.UserInfo = $"Welcome {user.UserName}";
            else
                ViewBag.UserInfo = "User not found.";

            return View();
        }
    }
}
