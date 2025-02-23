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

            var activityFromDb = await _context.Activities
                                .Where(t => t.ActivityId == activity_id)
                                .Select(t => new ActivityCardViewModel
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
            }).FirstOrDefaultAsync();

            if (activityFromDb == null) return NotFound();

            var announcementFromDb = await _context.Annoucements
                                    .Where(announce => announce.ActivityId == activity_id)
                                    .Select(announce => new AnnouncementViewModel
            {
                ActivityId = announce.ActivityId,
                AnnouncementId = announce.AnnoucementId,
                AnnounceDate = announce.AnnouceDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                Content = announce.Content            
            }).ToListAsync();;

            var model = new ViewActivityViewModel
            {
                Card = activityFromDb,
                Announcements = announcementFromDb
            };

            if (user != null)
            {
                model.UserName = user.UserName;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnounce(
            int ActivityId,
            string Content)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Json(new { success = false, message = "User not logged in." });
                
                var activity = _context.Activities.Find(ActivityId);
                if (activity == null) return NotFound("Activity not found.");

                var announcement = new Annoucement
                {
                    ActivityId = ActivityId,
                    Activity = activity,
                    AnnouceDate = DateTime.Now,
                    Content = Content
                };

                _context.Annoucements.Add(announcement);
                await _context.SaveChangesAsync();

                return Json(new { success = true});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> DeleteAnnounce(int annoucement_id)
        {
            try
            {
                var dl_announcement = _context.Annoucements.Find(annoucement_id);         
                if (dl_announcement == null) return NotFound("Announcement not found.");

                _context.Annoucements.Remove(dl_announcement);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
