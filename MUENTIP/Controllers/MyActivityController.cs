using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.ViewModels;
using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;

namespace MUENTIP.Controllers
{
    public class MyActivityController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public MyActivityController(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); // ดึง user ที่ login อยู่

            List<ActivityCardViewModel> activityFromDb = new(); // ตั้งค่าเริ่มต้นให้ไม่เป็น null

            if (user != null) // ตรวจสอบว่า user มีค่าหรือไม่
            {
                activityFromDb = await _context.Activities
                    .Where(t => t.User.Id == user.Id) // เลือกเฉพาะ activities ที่ถูกสร้างโดย user นี้
                    .Select(t => new ActivityCardViewModel
                    {
                        ActivityId = t.ActivityId,
                        Title = t.Title,
                        Owner = t.User.UserName,
                        Location = t.Location,
                        StartDateTime = t.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        EndDateTime = t.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        DeadlineDateTime = t.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        ApplyMax = t.ApplyMax,
                        ApplyCount = t.Applications.Count(),
                        TagsList = t.ActivityTags != null
                            ? t.ActivityTags.Select(at => at.Tag.TagName).ToList()
                            : new List<string>()
                    })
                    .ToListAsync(); // ✅ ใช้ async
            }

            var sampleApprovedActivities = new List<ActivityCardViewModel>
            {
                new ActivityCardViewModel
                {
                    ActivityId = 4,
                    Title = "driving",
                    Owner = "Inw111",
                    Location = "Mt. Olympus",
                    PostDateTime = "2025-03-09 10:00",
                    StartDateTime = "2025-03-15 10:00",
                    EndDateTime = "2025-03-15 13:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 7,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"}
                }
            };

            var sampleNonApprovedActivities = new List<ActivityCardViewModel>
            {
                new ActivityCardViewModel
                {
                    ActivityId = 7,
                    Title = "Hiking",
                    Owner = "Inw111",
                    Location = "Mt. Olympus",
                    PostDateTime = "2025-03-09 10:00",
                    StartDateTime = "2025-03-15 10:00",
                    EndDateTime = "2025-03-15 13:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 7,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"}
                }
            };

            var tagsFromDb = await _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToListAsync(); // ✅ ใช้ async

            //สร้าง viewModel 
            var model = new MyActivityViewModel
            {
                userId = int.TryParse(user?.Id, out int id) ? id : 0, // ✅ แปลง string -> int
                createdActivity = activityFromDb,
                approvedActivity = sampleApprovedActivities,
                nonApproveActivity = sampleNonApprovedActivities,
                filterTags = tagsFromDb
            };

            return View(model);
        }
    }
}
