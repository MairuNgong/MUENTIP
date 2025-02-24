using Microsoft.AspNetCore.Mvc;
using MUENTIP.Models;
using MUENTIP.ViewModels;

namespace MUENTIP.Controllers
{
    public class MyProfileController : Controller
    {
        // GET: MyProfileController
        private static MyProfile userProfile = new MyProfile
        {
            Id = 1,
            Picture = "https://th.bing.com/th/id/OIP.7OUNIFX2kCeRZJ4cv2dIBwHaE8?rs=1&pid=ImgDetMain",
            Email = "Som@gmail.com",
            Name = "ss",
            Detail = "ผมรักเมืองไทยมาอยู่เมืองไทยหลายปี",
            Birthday = new DateTime(1982, 10, 22),
            Gender = Gender.Male,
            Education = "ปริญญาตรี",
            Address = "Paris",
            Interest = new List<TagFilterViewModel>
            {
                new TagFilterViewModel { TagName = "Cook" },
                new TagFilterViewModel { TagName = "Food" }
            }
        };
        public IActionResult Index()
        {
            // สร้างตัวแปร MyProfile และกำหนดค่า
            

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
            var sampleApprovedActivities = new List<ActivityCardViewModel>
            {
                new ActivityCardViewModel
                {
                    ActivityId = 4,
                    Title = "Hiking",
                    Owner = "Inw111",
                    Location = "Mt. Olympus",
                    StartDateTime = "2025-03-08 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 7,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 5,
                    Title = "Swimming",
                    Owner = "Inw111",
                    Location = "Pool Olympus",
                    StartDateTime = "2025-03-08 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 10,
                    ApplyMax = 12,
                    TagsList = new List<string> {"Sport","Wellness"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 6,
                    Title = "Cooking class",
                    Owner = "Inw111",
                    Location = "Kitchen. Olympus",
                    StartDateTime = "2025-04-08 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 5,
                    ApplyMax = 5,
                    TagsList = new List<string> {"Cook","Food"}
                }
            };
            var sampleNonApprovedActivities = new List<ActivityCardViewModel>
            {
                new ActivityCardViewModel
                {
                    ActivityId = 7,
                    Title = "Hiking",
                    Owner = "Inw111",
                    Location = "Mt. Olympus",
                    StartDateTime = "2025-03-08 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-02-01 00:00",
                    ApplyCount = 7,
                    ApplyMax = 11,
                    TagsList = new List<string> {"Sport","Nature","Wellness"}
                },
                new ActivityCardViewModel
                {
                    ActivityId = 8,
                    Title = "Swimming",
                    Owner = "Inw111",
                    Location = "Pool Olympus",
                    StartDateTime = "2025-03-08 10:00",
                    EndDateTime = "2025-03-15 10:00",
                    DeadlineDateTime = "2025-02-20 00:00",
                    ApplyCount = 10,
                    ApplyMax = 12,
                    TagsList = new List<string> {"Sport"}
                }
            };
            
            var model = new MyActivityViewModel
            {
                userId = 1,
                createdActivity = sampleCreatedActivities,
                approvedActivity = sampleApprovedActivities,
                nonApproveActivity = sampleNonApprovedActivities
                
            };


            var viewModel = new MyProfileWithActivitiesViewModel
            {
                UserProfile = userProfile,
                ActivityViewModel = model
               
            };

            return View(viewModel);
        }

        public IActionResult EditProfile()
        {
            return View(userProfile);
        }
    }
}
