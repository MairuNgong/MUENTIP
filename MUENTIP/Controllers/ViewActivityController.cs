using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.ViewModels;
using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace MUENTIP.Controllers
{
    public class ViewActivityController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly EmailService _emailService;
        public ViewActivityController(ApplicationDBContext context, UserManager<User> userManager, EmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
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

            var participants = await _context.ParticipateIn
                .Where(p => p.ActivityId == activity_id)
                .ToListAsync();

            var is_selected = participants.Any();

            var participationStatus = "";

            if (!participants.Any())
            {
                participationStatus = "Not Yet";
            }
            else if (user != null)
            {
                participationStatus = participants.Any(p => p.UserId == user.Id) ? "Participating" : "Not Participating";
            }
            else
            {
                participationStatus = "Not Participating";
            }

            DateTime.TryParseExact(
                activityFromDb.DeadlineDateTime,
                "yyyy-MM-ddTHH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime deadline
            );

            bool out_of_date = deadline != default && DateTime.Now > deadline;

            var ownerInfo = await _userManager.Users
                .Where(u => u.UserName == activityFromDb.Owner)
                .Select(u => new { u.ProfileImageLink, u.Id })
                .FirstOrDefaultAsync();

            var ownerImg = ownerInfo?.ProfileImageLink ?? "../img/default-profile.png";
            var ownerId = ownerInfo?.Id;

            var model = new ViewActivityViewModel
            {
                Card = activityFromDb,
                OwnerImg = ownerImg,
                OwnerId = ownerId,
                Announcements = announcementFromDb,
                UserName = user?.UserName,
                IsApplyOn = is_applied ? (bool?)true : (bool?)false,
                ParticipationStatus = participationStatus,
                OutOfDate = out_of_date,
                IsSelected = is_selected
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnnounce(int ActivityId, string Content)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return Json(new { success = false, message = "User not logged in." });

                var activity = await _context.Activities
                    .Include(a => a.User) 
                    .FirstOrDefaultAsync(a => a.ActivityId == ActivityId);
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

                var participantEmails = await _context.ParticipateIn
                    .Where(p => p.ActivityId == ActivityId)
                    .Select(p => p.User.Email)
                    .ToListAsync();

                string subject = $"New Announcement for {activity.Title}";
                string body = $"Hello, <br/><br/>A new announcement has been posted for the activity <b>{activity.Title}</b>:<br/><br/>{Content}<br/><br/>Regards,<br/>MUENTIP Team";

                foreach (var email in participantEmails)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        await _emailService.SendEmailAsync(email, subject, body);
                    }
                }

                return Json(new { success = true });
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