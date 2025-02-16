using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MUENTIP.Controllers
{
    public class CreateActivityController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public CreateActivityController(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Fetch tags from the database
            var tagsFromDb = _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToList();

            var model = new CreateActivityViewModel
            {
                Tags = tagsFromDb
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
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
            string? selected_tags)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Json(new { success = false, message = "User not authenticated" });
                }
                if(DateTime.Parse($"{StartDate} {StartTime}") < DateTime.Now || DateTime.Parse($"{EndDate} {EndTime}") < DateTime.Now || DateTime.Parse($"{DeadlineDate} {DeadLineTime}") < DateTime.Now)
                {
                    return Json(new { success = false, message = "Date-Time input is already the in the past"});
                }
                if (DateTime.Parse($"{StartDate} {StartTime}") > DateTime.Parse($"{EndDate} {EndTime}"))
                {
                    return Json(new { success = false, message = "Activity can't end before start" });
                }
                if (DateTime.Parse($"{DeadlineDate} {DeadLineTime}") - DateTime.Now <= TimeSpan.FromMinutes(30))
                {
                    return Json(new { success = false, message = "Deadline needs to be at least 30 min." });
                }
                var activity = new Activity
                {
                    Title = Title,
                    StartDateTime = DateTime.Parse($"{StartDate} {StartTime}"),
                    EndDateTime = DateTime.Parse($"{EndDate} {EndTime}"),
                    DeadlineDateTime = DateTime.Parse($"{DeadlineDate} {DeadLineTime}"),
                    Location = Location,
                    UserId = user.Id,
                    ApplyMax = int.Parse(ApplyMax),
                    Description = Description,
                    PostDateTime = DateTime.Now
                };
                
                
                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();



                if (!string.IsNullOrEmpty(selected_tags))
                {
                    // Deserialize the JSON string into a list of Tag objects
                    List<Tag> tags = JsonConvert.DeserializeObject<List<Tag>>(selected_tags);

                    foreach (var tag in tags)
                    {
                        // Check if the tag exists in the database
                        var tagExists = await _context.Tags.AnyAsync(t => t.TagName == tag.TagName);
                        if (!tagExists)
                        {
                            return Json(new { success = false, message = $"Tag '{tag.TagName}' does not exist." });
                        }

                        // Create ActivityType entry
                        var activityType = new ActivityType
                        {
                            ActivityId = activity.ActivityId,
                            Activity = activity,
                            TagName = tag.TagName
                        };

                        _context.ActivityTypes.Add(activityType);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }

                return Json(new { success = true});
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "an error occurs." });
            }
        }
    }
}
