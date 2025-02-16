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
                    Title = "Badminton together",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    StartDateTime = "2025-03-15 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-03-10 10:00",
                    ApplyCount = 1,
                    ApplyMax = 3,
                    TagsList = new List<string> {"Sport"}
                },

                new ActivityCardViewModel
                {
                    ActivityId = 2,
                    Title = "Basketball",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    StartDateTime = "2025-03-18 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-03-15 00:00",
                    ApplyCount = 20,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 3,
                    Title = "Tech sharing",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    StartDateTime = "2025-01-18 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-01-15 00:00",
                    ApplyCount = 0,
                    ApplyMax = 30,
                    TagsList = new List<string> {"Technology"}
                }
            };
            var sampleNonApprovedActivities = new List<ActivityCardViewModel>
            {
                new ActivityCardViewModel
                {
                    ActivityId = 1,
                    Title = "Badminton together",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    StartDateTime = "2025-03-15 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-03-10 10:00",
                    ApplyCount = 1,
                    ApplyMax = 3,
                    TagsList = new List<string> {"Sport"}
                },

                new ActivityCardViewModel
                {
                    ActivityId = 2,
                    Title = "Basketball",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    StartDateTime = "2025-03-18 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-03-15 00:00",
                    ApplyCount = 20,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 3,
                    Title = "Tech sharing",
                    Owner = "Inwza007",
                    Location = "badminton court, kmitl",
                    StartDateTime = "2025-01-18 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-01-15 00:00",
                    ApplyCount = 0,
                    ApplyMax = 30,
                    TagsList = new List<string> {"Technology"}
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
                approvedActivity = sampleAppro vedActivities,
                nonApproveActivity = sampleNonApprovedActivities,
                FilterTags = sampleTags
            };
            return View(model);
        }
    }
}
