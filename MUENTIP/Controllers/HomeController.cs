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

                new ActivityCardViewModel
                {
                    ActivityId = 1,
                    Title = "ตีแบตกันเว้ยเฮีย ด่วนๆๆๆๆๆมาก",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    PostDateTime = "2025-03-09 10:00",
                    StartDateTime = "2025-03-15 10:00",
                    EndDateTime = "2025-03-15 13:00",
                    DeadlineDateTime = "2025-03-10 10:00",
                    ApplyCount = 1,
                    ApplyMax = 3,
                    TagsList = new List<string> {"Sport", "Badminton"},
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                },

                new ActivityCardViewModel
                {
                    ActivityId = 2,
                    Title = "Basketball",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    PostDateTime = "2025-03-14 00:00",
                    StartDateTime = "2025-03-18 10:00",
                    EndDateTime = "2025-03-19 13:00",
                    DeadlineDateTime = "2025-03-15 00:00",
                    ApplyCount = 20,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport"},
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                },
                new ActivityCardViewModel
                {
                    ActivityId = 3,
                    Title = "Tech sharing",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    PostDateTime = "2025-01-14 00:00",
                    StartDateTime = "2025-01-18 10:00",
                    EndDateTime = "2025-01-21 13:00",
                    DeadlineDateTime = "2025-01-15 00:00",
                    ApplyCount = 0,
                    ApplyMax = 30,
                    TagsList = new List<string> {"Technology"},
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                },
                new ActivityCardViewModel
                {
                    ActivityId = 1,
                    Title = "Badminton together",
                    Owner = "Inwza007",
                    Location = "Mt. Olympus",
                    PostDateTime = "2025-02-14 00:00",
                    StartDateTime = "2025-03-08 10:00",
                    EndDateTime = "2025-03-08 13:00",
                    DeadlineDateTime = "2025-02-15 00:00",
                    ApplyCount = 7,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"},
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                },
                new ActivityCardViewModel
                {
                    ActivityId = 3,
                    Title = "Tech sharing",
                    Owner = "Inwza007",
                    Location = "Pool Olympus",
                    PostDateTime = "2025-01-31 00:00",
                    StartDateTime = "2025-03-08 10:00",
                    EndDateTime = "2025-03-08 13:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 10,
                    ApplyMax = 12,
                    TagsList = new List<string> {"Sport","Wellness"},
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                },
                new ActivityCardViewModel
                {
                    ActivityId = 6,
                    Title = "Cooking class",
                    Owner = "Inwza007",
                    Location = "Kitchen. Olympus",
                    PostDateTime = "2025-02-28 00:00",
                    StartDateTime = "2025-04-08 10:00",
                    EndDateTime = "2025-08-11 13:00",
                    DeadlineDateTime = "2025-03-01 00:00",
                    ApplyCount = 5,
                    ApplyMax = 5,
                    TagsList = new List<string> {"Cook","Food"},
                    Description = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
                }
            };
