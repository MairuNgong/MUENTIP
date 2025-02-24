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

            bool is_applied = false;
            if (user != null)
            {
                is_applied = await _context.ApplyOn
                    .AnyAsync(a => a.UserId == user.Id && a.ActivityId == activity_id);
            }

            var participationStatus = "Not Yet";
            var participants = await _context.ParticipateIn
                .Where(p => p.ActivityId == activity_id)
                .ToListAsync();

            if (participants != null && user != null)
            {
                var participant = await _context.ParticipateIn
                    .FirstOrDefaultAsync(p => p.UserId == user.Id && p.ActivityId == activity_id);
                
                if (participant != null)
                {
                    participationStatus = "Participating";
                }
                else
                {
                    participationStatus = "Not Participating";
                }
            }

            DateTime deadline;
            bool isValidDeadline = DateTime.TryParseExact(
                activityFromDb.DeadlineDateTime,
                "yyyy-MM-ddTHH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out deadline
            );

            bool out_of_date = isValidDeadline && DateTime.Compare(DateTime.Now, deadline) > 0;

            var model = new ViewActivityViewModel
            {
                Card = activityFromDb,
                Announcements = announcementFromDb,
                UserName = user?.UserName,
                IsApplyOn = is_applied ? (bool?)true : (bool?)false, 
                ParticipationStatus = string.IsNullOrEmpty(participationStatus) ? "Not Participating" : participationStatus,  // Default to "Not Participating"
                OutOfDate = out_of_date
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnounce(int activity_id, string content)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Json(new { success = false, message = "User not logged in." });
                
                var activity = _context.Activities.Find(activity_id);
                if (activity == null) return NotFound("Activity not found.");

                var announcement = new Annoucement
                {
                    ActivityId = activity_id,
                    Activity = activity,
                    AnnouceDate = DateTime.Now,
                    Content = content
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

        public async Task<IActionResult> ApplyOn(int activity_id){
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Json(new { success = false, message = "User not logged in." });
                
                var activity = _context.Activities.Find(activity_id);
                if (activity == null) return NotFound("Activity not found.");

                var apply_on = new ApplyOn
                {
                    ActivityId = activity_id,
                    Activity = activity,
                    UserId = user.Id,
                    User = user,
                    AppliedDate = DateTime.Now,
                };

                _context.ApplyOn.Add(apply_on);
                await _context.SaveChangesAsync();

                return Json(new { success = true});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public async Task<IActionResult> Withdraw(int activity_id){
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Json(new { success = false, message = "User not logged in." });
                
                var apply_on = await _context.ApplyOn
                    .FirstOrDefaultAsync(a => a.UserId == user.Id && a.ActivityId == activity_id);
                if (apply_on == null) return Json(new { success = false, message = "User is not participating in this activity." });

                _context.ApplyOn.Remove(apply_on);
                await _context.SaveChangesAsync();

                return Json(new { success = true});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
