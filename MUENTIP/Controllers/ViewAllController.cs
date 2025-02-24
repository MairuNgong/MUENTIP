using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;

namespace MUENTIP.Controllers
{
    public class ViewAllController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ViewAllController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(string tag_name)
        {
            var activityFromDb = _context.Activities
                                .OrderByDescending(t => t.ActivityId) 
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
            }).ToList();

            var activity_cards = activityFromDb.Where(card => card.TagsList.Contains(tag_name)).ToList();
            if (!activity_cards.Any())
            {
                activity_cards = activityFromDb;
            }

            var model = new ViewAllViewModel
            {
                TagName = tag_name,
                Cards = activity_cards
            };

            return View(model);
        }
    }
}
