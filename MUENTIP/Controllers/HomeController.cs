using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;

namespace MVC_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
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

            var topTags = _context.ActivityTypes
                .GroupBy(at => at.Tag.TagName)
                .Select(g => new { TagName = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(6)
                .Select(g => new TagFilterViewModel { TagName = g.TagName })
                .ToList();

            var model = new HomeViewModel
            {
                Cards = activityFromDb,
                Tags = topTags 
            };

            return View(model);
        }
    }
}
