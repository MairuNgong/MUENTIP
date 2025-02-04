using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MUENTIP.ViewModels;
using MVC_test.Models;

namespace MVC_test.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            var sampleCards = new List<ActivityCardViewModel>
            {
                new ActivityCardViewModel
                {
                    ActivityId = 1,
                    Title = "Badminton together",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    ActivityDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-03-10 10:00",
                    ApplyCount = 1,
                    ApplyMax = 3,
                    TagsList = new List<string> {"Sport", "Badminton"}
                },

                new ActivityCardViewModel
                {
                    ActivityId = 2,
                    Title = "Basketball",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    ActivityDateTime = "2025-03-18 10:00",
                    DeadlineDateTime = "2025-03-15 00:00",
                    ApplyCount = 20,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 3,
                    Title = "Tech sharing",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    ActivityDateTime = "2025-01-18 10:00",
                    DeadlineDateTime = "2025-01-15 00:00",
                    ApplyCount = 0,
                    ApplyMax = 30,
                    TagsList = new List<string> {"Technology"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 4,
                    Title = "Hiking",
                    Owner = "Inwza007",
                    Location = "Mt. Olympus",
                    ActivityDateTime = "2025-03-08 10:00",
                    DeadlineDateTime = "2025-02-15 00:00",
                    ApplyCount = 7,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 5,
                    Title = "Swimming",
                    Owner = "Inwza007",
                    Location = "Pool Olympus",
                    ActivityDateTime = "2025-03-08 10:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 10,
                    ApplyMax = 12,
                    TagsList = new List<string> {"Sport","Wellness"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 6,
                    Title = "Cooking class",
                    Owner = "Inwza007",
                    Location = "Kitchen. Olympus",
                    ActivityDateTime = "2025-04-08 10:00",
                    DeadlineDateTime = "2025-03-01 00:00",
                    ApplyCount = 5,
                    ApplyMax = 5,
                    TagsList = new List<string> {"Cook","Food"}
                }
            };

            var sampleTags = new List<TagFilterViewModel>
            {
                new TagFilterViewModel
                {
                    TagName = "Cook"
                },
                new TagFilterViewModel
                {
                    TagName = "Food"
                },
                new TagFilterViewModel
                {
                    TagName = "Sport"
                },
                new TagFilterViewModel
                {
                    TagName = "Wellness"
                },
                new TagFilterViewModel
                {
                    TagName = "Nature"
                },
                new TagFilterViewModel
                {
                    TagName = "Technology"
                }
            };

            var model = new HomeViewModel
            {
                Cards = sampleCards,
                Tags = sampleTags
            };
            return View(model);
        }
    }
}
