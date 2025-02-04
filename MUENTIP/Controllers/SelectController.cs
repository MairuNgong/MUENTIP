using Microsoft.AspNetCore.Mvc;
using MUENTIP.ViewModels;
using static System.Net.WebRequestMethods;

namespace MUENTIP.Controllers
{
    public class SelectController : Controller
    {
        public IActionResult Index()
        {
            var sampleApplier = new List<UserCardViewModel>
            {
                new UserCardViewModel { userId = 1, userName = "John Doe", userImgLink = "https://i.pravatar.cc/150?img=1", email = "john.doe@example.com" },
                new UserCardViewModel { userId = 2, userName = "Jane Smith", userImgLink = "https://i.pravatar.cc/150?img=2", email = "jane.smith@example.com" },
                new UserCardViewModel { userId = 3, userName = "Alice Johnson", userImgLink = "https://i.pravatar.cc/150?img=3", email = "alice.johnson@example.com" },
                new UserCardViewModel { userId = 4, userName = "Bob Brown", userImgLink = "https://i.pravatar.cc/150?img=4", email = "bob.brown@example.com" },
                new UserCardViewModel { userId = 5, userName = "Charlie Wilson", userImgLink = "https://i.pravatar.cc/150?img=5", email = "charlie.wilson@example.com" },
                new UserCardViewModel { userId = 6, userName = "David Martinez", userImgLink = "https://i.pravatar.cc/150?img=6", email = "david.martinez@example.com" },
                new UserCardViewModel { userId = 7, userName = "Emma Garcia", userImgLink = "https://i.pravatar.cc/150?img=7", email = "emma.garcia@example.com" },
                new UserCardViewModel { userId = 8, userName = "Franklin Evans", userImgLink = "https://i.pravatar.cc/150?img=8", email = "franklin.evans@example.com" }
            };
            var model = new SelectViewModel
            {
                ownerId = 9,
                ownerImgLink = "https://i.pravatar.cc/150?img=9",
                applyMax = 5,
                Appliers = sampleApplier
            };
            return View(model);
        }
    }
}
