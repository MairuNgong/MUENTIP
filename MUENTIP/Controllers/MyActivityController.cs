using Microsoft.AspNetCore.Mvc;
using MUENTIP.ViewModels;

namespace MUENTIP.Controllers
{
    public class MyActivityController : Controller
    {
        public IActionResult Index()
        {
            var sampleCreatedActivities = new List<ActivityCardViewModel>
            {
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
                }
            };
            var sampleApprovedActivities = new List<ActivityCardViewModel>
            {
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
                }
            };
            var sampleNonApprovedActivities = new List<ActivityCardViewModel>
            {
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
            var model = new MyActivityViewModel
            {
                userId = 1,
                createdActivity = sampleCreatedActivities,
                approvedActivity = sampleApprovedActivities,
                nonApproveActivity = sampleNonApprovedActivities,
                FilterTags = sampleTags
            };
            return View(model);
        }
    }
}
