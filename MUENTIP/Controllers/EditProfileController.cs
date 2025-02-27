using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.ViewModels;
using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;

namespace MUENTIP.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public EditProfileController(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var userClass = await _context.Users
                .Include(u => u.InterestedTags)
                .ThenInclude(it => it.Tag)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            var tagsFromDb = await _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToListAsync();

            var model = new MyProfileViewModel
            {
                UserName = user.UserName,
                ProfileImageLink = user.ProfileImageLink ?? "https://th.bing.com/th/id/OIP.aPBOwj8LDbYM2vElJgv_SQAAAA?rs=1&pid=ImgDetMain",
                Email = user.Email,
                Info = user.Info,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Education = user.Education,
                Address = user.Address,
                InterestedTags = user.InterestedTags?.Select(it => it.TagName).ToList() ?? new List<string>(),
                availableTags = tagsFromDb
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(MyProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Update user information
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Info = model.Info;
                user.BirthDate = model.BirthDate;
                user.Gender = model.Gender;
                user.Education = model.Education;
                user.Address = model.Address;

                // Tags ที่ผู้ใช้เลือก
                var tags = model.InterestedTags;  // ใช้ List<string> ตรงๆ

                var tagsFromDb = await _context.Tags
                    .Where(t => tags.Contains(t.TagName))
                    .ToListAsync();

                var currentTags = await _context.InterestIn
                    .Where(interest => interest.UserId == user.Id)
                    .ToListAsync();

                // ลบ tags เก่าออก
                _context.InterestIn.RemoveRange(currentTags);

                // เพิ่ม tags ใหม่
                foreach (var tag in tagsFromDb)
                {
                    _context.InterestIn.Add(new InterestIn
                    {
                        UserId = user.Id,
                        TagName = tag.TagName,
                        Tag = tag  // เชื่อมโยง Tag กับ InterestIn
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

    }
}
