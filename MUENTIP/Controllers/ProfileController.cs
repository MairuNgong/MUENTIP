using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting.Hosting;
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
            if (user != null) { 
                ViewBag.Username = $"Welcome {user.UserName}";
                if(user.ProfileImageLink != null){
                    ViewBag.ProfileImageLink = user.ProfileImageLink;
                }
                else
                {
                    ViewBag.ProfileImageLink = "https://th.bing.com/th/id/OIP.aPBOwj8LDbYM2vElJgv_SQAAAA?rs=1&pid=ImgDetMain";
                }


                    ViewBag.Email = user.Email;
                ViewBag.Info = user.Info;
                ViewBag.BirthDate = user.BirthDate;
                ViewBag.Gender = user.Gender;
                ViewBag.Education = user.Education;
                ViewBag.Address = user.Address;
                ViewBag.CreatedActivities = user.CreatedActivities;
            }



            else {
                ViewBag.Username = "User not found."; }
            


            return View();
        }
    }
}
