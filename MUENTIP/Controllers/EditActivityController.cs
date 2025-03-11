using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;
using Newtonsoft.Json;

namespace MUENTIP.Controllers
{
    public class EditActivityController : Controller
    {

        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public EditActivityController(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(int id)
        {
            var activity = await _context.Activities.Include(a => a.ActivityTags).FirstOrDefaultAsync(a => a.ActivityId == id);
            if (activity == null)
            {
                return NotFound();
            }

            if (activity.DeadlineDateTime <= DateTime.Now) {
                return Unauthorized();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null || user.Id != activity.UserId)
            {
                return Unauthorized();
            }

            var model = new EditActivityViewModel
            {
                ActivityId = activity.ActivityId,
                Title = activity.Title,
                StartDateTime = activity.StartDateTime,
                EndDateTime = activity.EndDateTime,
                DeadlineDateTime = activity.DeadlineDateTime,
                Location = activity.Location,
                ApplyMax = activity.ApplyMax,
                Description = activity.Description,
                ActivityTags = activity.ActivityTags != null ? activity.ActivityTags.Select(t => t.TagName).ToList() : new List<string>(),
                AllTags = _context.Tags.Select(t => t.TagName).ToList()

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(
            string Title,
            string StartDate,
            string StartTime,
            string EndDate,
            string EndTime,
            string DeadlineDate,
            string DeadLineTime,
            string Location,
            string ApplyMax,
            string? Description,
            string? selected_tags,
            string activityId)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (DateTime.Parse($"{StartDate} {StartTime}") < DateTime.Now || DateTime.Parse($"{EndDate} {EndTime}") < DateTime.Now || DateTime.Parse($"{DeadlineDate} {DeadLineTime}") < DateTime.Now)
                {
                    return Json(new { success = false, message = "Date-Time input is already the in the past" });
                }
                if (DateTime.Parse($"{StartDate} {StartTime}") > DateTime.Parse($"{EndDate} {EndTime}"))
                {
                    return Json(new { success = false, message = "Activity can't end before start" });
                }
                if (DateTime.Parse($"{DeadlineDate} {DeadLineTime}") - DateTime.Now <= TimeSpan.FromMinutes(30))
                {
                    return Json(new { success = false, message = "Deadline needs to be at least 30 min." });
                }
                var existingActivity = await _context.Activities.FindAsync(int.Parse(activityId));

                if (existingActivity != null)
                {

                    existingActivity.Title = Title;
                    existingActivity.StartDateTime = DateTime.Parse($"{StartDate} {StartTime}");
                    existingActivity.EndDateTime = DateTime.Parse($"{EndDate} {EndTime}");
                    existingActivity.DeadlineDateTime = DateTime.Parse($"{DeadlineDate} {DeadLineTime}");
                    existingActivity.Location = Location;
                    existingActivity.UserId = user.Id;
                    existingActivity.ApplyMax = int.Parse(ApplyMax);
                    existingActivity.Description = Description;
                    existingActivity.PostDateTime = DateTime.Now;


                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Json(new { success = false, message = "No activity found in database" });
                }



                if (!string.IsNullOrEmpty(selected_tags))
                {

                    List<string> tagsList = JsonConvert.DeserializeObject<List<string>>(selected_tags);

                    _context.ActivityTypes.RemoveRange(
                        _context.ActivityTypes.Where(at => at.ActivityId == existingActivity.ActivityId)
                    );

                    foreach (var tag in tagsList)
                    {
                 
                        var tagExists = await _context.Tags.AnyAsync(t => t.TagName == tag);
                        if (!tagExists)
                        {
                            return Json(new { success = false, message = $"Tag '{tag}' does not exist." });
                        } 

                        var activityType = new ActivityType
                        {
                            ActivityId = existingActivity.ActivityId,
                            Activity = existingActivity,
                            TagName = tag
                        };

                        _context.ActivityTypes.Add(activityType);
                    }

                    await _context.SaveChangesAsync();
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"{selected_tags}" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Close(int id)
        {
            try
            {
                var activity = await _context.Activities.FirstOrDefaultAsync(a => a.ActivityId == id);

                if (activity == null)
                {
                    return NotFound();
                }
               
                activity.DeadlineDateTime = DateTime.UtcNow.AddDays(-1);
                
                _context.Activities.Update(activity);
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
