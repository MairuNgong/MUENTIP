using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;

namespace MUENTIP.Controllers
{
    public class SearchPageController : Controller
    {
       private readonly ApplicationDBContext _context;

        public SearchPageController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var activityFromDb = _context.Activities.OrderByDescending(a => a.ActivityId).Select(t => new ActivityCardViewModel
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
                TagsList = t.ActivityTags != null ? t.ActivityTags.Select(at => at.Tag.TagName).ToList() : new List<string>()
            }).ToList();

            var tagsFromDb = _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToList();

            var model = new SearchViewModel
                {
                    Cards = activityFromDb,
                    Tags = tagsFromDb
            };

            return View(model);
        }
    }
}
