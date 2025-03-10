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
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            // ดึงเฉพาะ userId ไปใช้ในการ Query เพื่อให้ประสิทธิภาพดีขึ้น
            var userId = user.Id;

            // ใช้ Task.WhenAll() เพื่อให้ทุก Query ดำเนินไปพร้อมกัน
            var createdActivitiesTask = _context.Activities
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.ActivityId)
                .Select(a => new ActivityCardViewModel
                {
                    ActivityId = a.ActivityId,
                    Title = a.Title,
                    Owner = a.User.UserName,
                    Location = a.Location,
                    StartDateTime = a.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    EndDateTime = a.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    DeadlineDateTime = a.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ApplyMax = a.ApplyMax,
                    ApplyCount = a.Applications.Count(),
                    TagsList = a.ActivityTags.Select(at => at.Tag.TagName).ToList()
                })
                .ToListAsync();

            var applicationsTask = _context.ApplyOn
                .AsNoTracking()
                .Where(a => a.UserId == userId && a.Activity != null)
                .OrderByDescending(a => a.Activity.ActivityId)
                .Select(apply => new ActivityCardViewModel
                {
                    ActivityId = apply.Activity.ActivityId,
                    Title = apply.Activity.Title,
                    Owner = apply.Activity.User.UserName,
                    Location = apply.Activity.Location,
                    StartDateTime = apply.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    EndDateTime = apply.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    DeadlineDateTime = apply.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ApplyMax = apply.Activity.ApplyMax,
                    ApplyCount = apply.Activity.Applications.Count(),
                    TagsList = apply.Activity.ActivityTags.Select(at => at.Tag.TagName).ToList()
                })
                .ToListAsync();

            var participationsTask = _context.ParticipateIn
                .AsNoTracking()
                .Where(p => p.UserId == userId && p.Activity != null)
                .OrderByDescending(p => p.Activity.ActivityId)
                .Select(p => new ActivityCardViewModel
                {
                    ActivityId = p.Activity.ActivityId,
                    Title = p.Activity.Title,
                    Owner = p.Activity.User.UserName,
                    Location = p.Activity.Location,
                    StartDateTime = p.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    EndDateTime = p.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    DeadlineDateTime = p.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ApplyMax = p.Activity.ApplyMax,
                    ApplyCount = p.Activity.Applications.Count(),
                    TagsList = p.Activity.ActivityTags.Select(at => at.Tag.TagName).ToList()
                })
                .ToListAsync();

            var tagsTask = _context.Tags
                .AsNoTracking()
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToListAsync();

            // รอให้ทุก Task ทำงานเสร็จ
            await Task.WhenAll(createdActivitiesTask, applicationsTask, participationsTask, tagsTask);

            var model = new MyActivityViewModel
            {
                userId = int.TryParse(userId, out int id) ? id : 0,
                createdActivity = await createdActivitiesTask,
                nonApproveActivity = await applicationsTask,
                approvedActivity = await participationsTask,
                filterTags = await tagsTask
            };
            return View(model);
        }
        
    }
}