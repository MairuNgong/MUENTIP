using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;

namespace MVC_test.Controllers
{
    public class ActivityCardController1 : Controller
    {

        private readonly ApplicationDBContext _context;

        public ActivityCardController1(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
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
                Description = t.Description,
                TagsList = t.ActivityTags != null ? t.ActivityTags.Select(at => at.Tag.TagName).ToList() : new List<string>()
            }).ToList();

            var tagsFromDb = _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToList();

            var model = new TestActivityCard_FilterViewModel
                {
                    Cards = activityFromDb,
                    Tags = tagsFromDb
            };

            return View(model);
        }
    }
}
