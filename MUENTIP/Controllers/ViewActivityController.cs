using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.ViewModels;
using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;

namespace MUENTIP.Controllers
{
    public class ViewActivityController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public ViewActivityController(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int activity_id)
        {
            var user = await _userManager.GetUserAsync(User);

            var activityFromDb = _context.Activities.Select(t => new ActivityCardViewModel
            {
                ActivityId = t.ActivityId,
                Title = t.Title,
                Owner = t.User.UserName,
                Location = t.Location,
                PostDateTime = t.PostDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                StartDateTime = t.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                EndDateTime = t.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                DeadlineDateTime = t.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                ApplyMax = t.ApplyMax,
                ApplyCount = t.Applications.Count(),
                TagsList = t.ActivityTags != null ? t.ActivityTags.Select(at => at.Tag.TagName).ToList() : new List<string>(),
                Description = t.Description
            }).ToList();

            var announcementFromDb = _context.Annoucements.Select(announce => new AnnouncementViewModel
            {
                ActivityId = announce.ActivityId,
                AnnouncementId = announce.AnnoucementId,
                AnnounceDate = announce.AnnouceDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                Content = announce.Content            
            }).ToList();

            var activity_card = activityFromDb.FirstOrDefault(act_card => act_card.ActivityId == activity_id);
            if (activity_card == null) return NotFound();

            var announcements = announcementFromDb.Where(announce => announce.ActivityId == activity_id).ToList();

            ViewActivityViewModel model;

            if (user == null)
            {
                model = new ViewActivityViewModel
                {
                    Card = activity_card,
                    Announcements = announcements
                };
            }
            else
            {
                model = new ViewActivityViewModel
                {
                    UserId = user.Id,
                    Card = activity_card,
                    Announcements = announcements
                };
            }

            return View(model);
        }

    }
}
