using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MUENTIP.Models;
using MUENTIP.ViewModels;
using MVC_test.Models;

namespace MUENTIP.Controllers
{
    public class ViewActivityController : Controller
    {
        public IActionResult Index(int activity_id)
        {
            var sampleCards = new List<ActivityCardViewModel>
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
                    ActivityId = 4,
                    Title = "Hiking",
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
                    ActivityId = 5,
                    Title = "Swimming",
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

            var sampleAnnouncements = new List<AnnouncementViewModel>
            {
                new AnnouncementViewModel
                {
                    ActivityId = 1,
                    AnnouncementId = 1,
                    AnnounceDate = "2025-03-01 10:00",
                    Content = "Join us for a fun badminton session! Sign up now. 1"
                },
                new AnnouncementViewModel
                {
                    ActivityId = 1,
                    AnnouncementId = 2,
                    AnnounceDate = "2025-03-01 10:00",
                    Content = "Join us for a fun badminton session! Sign up now. 2"
                },
                new AnnouncementViewModel
                {
                    ActivityId = 1,
                    AnnouncementId = 3,
                    AnnounceDate = "2025-03-01 10:00",
                    Content = "Join us for a fun badminton session! Sign up now. 3"
                },
                new AnnouncementViewModel
                {
                    ActivityId = 2,
                    AnnouncementId = 1,
                    AnnounceDate = "2025-03-05 10:00",
                    Content = "Get ready for basketball! A few spots left."
                },
                new AnnouncementViewModel
                {
                    ActivityId = 3,
                    AnnouncementId = 1,
                    AnnounceDate = "2025-01-15 10:00",
                    Content = "Tech sharing event coming soon. Don't miss out on the insights!"
                },
                new AnnouncementViewModel
                {
                    ActivityId = 4,
                    AnnouncementId = 1,
                    AnnounceDate = "2025-02-20 10:00",
                    Content = "Prepare for an exciting hiking adventure. Limited spots available!"
                },
                new AnnouncementViewModel
                {
                    ActivityId = 5,
                    AnnouncementId = 1,
                    AnnounceDate = "2025-02-25 10:00",
                    Content = "Join our swimming session at Pool Olympus! Registration is open."
                }
            };

            var activity_card = sampleCards.FirstOrDefault(act_card => act_card.ActivityId == activity_id);
            if (activity_card == null) return NotFound();

            var announcements = sampleAnnouncements.Where(announce => announce.ActivityId == activity_id).ToList();
            if (!announcements.Any()) return NotFound();

            var model = new ViewActivityViewModel
            {
                Card = activity_card,
                Announcements = announcements
            };

            return View(model);
        }
    }
}
