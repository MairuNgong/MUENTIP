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

            if (user == null)
            {
                return NotFound(); ; // กลับไปหน้า login ถ้ายังไม่ได้ login
            }

            // โหลดข้อมูล User พร้อมกิจกรรมที่สร้างและกิจกรรมที่สมัครไว้
            var userClass = await _context.Users
                .Include(u => u.CreatedActivities)   // โหลดกิจกรรมที่ user สร้าง
                    .ThenInclude(a => a.Applications) // โหลด Applications ของแต่ละกิจกรรม
                .Include(u => u.CreatedActivities)
                    .ThenInclude(a => a.ActivityTags) // โหลด Tags ของกิจกรรม
                        .ThenInclude(at => at.Tag) // โหลดชื่อ Tag
                .Include(u => u.Applications) // โหลด ApplyOn (กิจกรรมที่ user สมัครไว้)
                    .ThenInclude(apply => apply.Activity) // โหลดข้อมูล Activity
                        .ThenInclude(a => a.User) // โหลดข้อมูล User เจ้าของกิจกรรม
                .Include(u => u.Applications) // โหลด ApplyOn (กิจกรรมที่ user สมัครไว้)
                    .ThenInclude(apply => apply.Activity) // โหลดข้อมูล Activity
                        .ThenInclude(a => a.ActivityTags) // โหลด Tags ของ Activity
                            .ThenInclude(at => at.Tag) // โหลดชื่อ Tag
                .Include(u => u.Participations) // โหลด Participations (กิจกรรมที่ได้รับการอนุมัติ)
                    .ThenInclude(p => p.Activity) // โหลดข้อมูล Activity ของการเข้าร่วม
                        .ThenInclude(a => a.User) // โหลดข้อมูล User เจ้าของกิจกรรม
                .FirstOrDefaultAsync(u => u.Id == user.Id);


            List<ActivityCardViewModel> CreatedActivityFromDb = new();
            List<ActivityCardViewModel> NonApprovedActivityFromDb = new();
            List<ActivityCardViewModel> ApprovedActivityFromDb = new();

            if (userClass != null)
            {
                CreatedActivityFromDb = userClass.CreatedActivities
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
                    .ToList();


                NonApprovedActivityFromDb = userClass.Applications
                    .Where(apply => apply.Activity != null)
                    .Select(apply => new ActivityCardViewModel
                    {
                        ActivityId = apply.ActivityId,
                        Title = apply.Activity.Title,
                        Owner = apply.Activity.User.UserName, // ดึงชื่อเจ้าของกิจกรรม
                        Location = apply.Activity.Location,
                        StartDateTime = apply.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        EndDateTime = apply.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        DeadlineDateTime = apply.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        ApplyMax = apply.Activity.ApplyMax,
                        ApplyCount = _context.ApplyOn.Count(a => a.ActivityId == apply.ActivityId),
                        TagsList = apply.Activity.ActivityTags?.Select(at => at.Tag.TagName).ToList() ?? new List<string>(),
                    })
                    .ToList();

                ApprovedActivityFromDb = userClass.Participations
                    .Where(p => p.Activity != null)
                    .Select(p => new ActivityCardViewModel
                    {
                        ActivityId = p.Activity.ActivityId,
                        Title = p.Activity.Title,
                        Owner = p.Activity.User.UserName ?? "Unknown", // ป้องกัน null
                        Location = p.Activity.Location,
                        StartDateTime = p.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        EndDateTime = p.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        DeadlineDateTime = p.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        ApplyMax = p.Activity.ApplyMax,
                        ApplyCount = _context.ApplyOn.Count(a => a.ActivityId == p.ActivityId),
                        TagsList = p.Activity.ActivityTags?.Select(at => at.Tag.TagName).ToList() ?? new List<string>()
                    })
                    .ToList();

                NonApprovedActivityFromDb.RemoveAll(n => ApprovedActivityFromDb.Any(a => a.ActivityId == n.ActivityId));
            }

            // ดึงแท็กจากฐานข้อมูล
            var tagsFromDb = await _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToListAsync();

            // สร้าง ViewModel
            var model = new MyActivityViewModel
            {
                userId = int.TryParse(user.Id, out int id) ? id : 0, // แปลง string -> int
                createdActivity = CreatedActivityFromDb,
                nonApproveActivity = NonApprovedActivityFromDb,
                approvedActivity = ApprovedActivityFromDb,
                filterTags = tagsFromDb
            };

            return View(model);
        }
    }
}
