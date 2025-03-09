using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.ViewModels;
using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
namespace MUENTIP.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public ProfileController(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string id)
        {

            var userClass = await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.UserName,
                    u.ProfileImageLink,
                    u.Email,
                    u.Info,
                    u.BirthDate,
                    u.Gender,
                    u.Education,
                    u.Address,
                    u.ShowCreate,
                    u.ShowParticipate,
                    InterestedTags = u.InterestedTags.Select(it => it.Tag.TagName).ToList(),

                    CreatedActivities = u.CreatedActivities.Select(a => new
                    {
                        a.ActivityId,
                        a.Title,
                        a.Location,
                        a.StartDateTime,
                        a.EndDateTime,
                        a.DeadlineDateTime,
                        a.ApplyMax,
                        ApplyCount = a.Applications.Count(),
                        Tags = a.ActivityTags.Select(at => at.Tag.TagName).ToList(),
                        OwnerName = a.User.UserName
                    }).ToList(),

                    Participations = u.Participations.Select(p => new
                    {
                        Activity = p.Activity != null ? new
                        {
                            p.Activity.ActivityId,
                            p.Activity.Title,
                            OwnerName = p.Activity.User.UserName,
                            p.Activity.Location,
                            p.Activity.StartDateTime,
                            p.Activity.EndDateTime,
                            p.Activity.DeadlineDateTime,
                            p.Activity.ApplyMax,
                            ApplyCount = p.Activity.Applications.Count(),
                            Tags = p.Activity.ActivityTags.Select(at => at.Tag.TagName).ToList()
                        } : null
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            List<ActivityCardViewModel> CreatedActivityFromDb = new();
            List<ActivityCardViewModel> NonApprovedActivityFromDb = new();
            List<ActivityCardViewModel> ApprovedActivityFromDb = new();

            if (userClass != null)
            {
                CreatedActivityFromDb = userClass.CreatedActivities
                    .Select(t => new ActivityCardViewModel
                    {
                        ActivityId = t.ActivityId,
                        Title = t.Title,
                        Owner = t.OwnerName,
                        Location = t.Location,
                        StartDateTime = t.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        EndDateTime = t.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        DeadlineDateTime = t.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        ApplyMax = t.ApplyMax,
                        ApplyCount = t.ApplyCount,
                        TagsList = t.Tags
                    })
                    .ToList();



                ApprovedActivityFromDb = userClass.Participations
                    .Where(p => p.Activity != null)
                    .Select(p => new ActivityCardViewModel
                    {
                        ActivityId = p.Activity.ActivityId,
                        Title = p.Activity.Title,
                        Owner = p.Activity.OwnerName,
                        Location = p.Activity.Location,
                        StartDateTime = p.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm"),
                        EndDateTime = p.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm"),
                        DeadlineDateTime = p.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm"),
                        ApplyMax = p.Activity.ApplyMax,
                        ApplyCount = p.Activity.ApplyCount,
                        TagsList = p.Activity.Tags
                    })
                    .ToList();
            }
            else
            {
                return NotFound();
            }
            var tagsFromDb = await _context.Tags
                        .Select(t => new TagFilterViewModel { TagName = t.TagName })
                        .ToListAsync();
            var model = new MyProfileViewModel
            {
                Id = userClass.Id,
                UserName = userClass.UserName,
                ProfileImageLink = userClass.ProfileImageLink,
                Email = userClass.Email,
                Info = userClass.Info,
                BirthDate = userClass.BirthDate,
                Gender = userClass.Gender,
                Education = userClass.Education,
                Address = userClass.Address,
                InterestedTags = userClass.InterestedTags,
                createdActivity = CreatedActivityFromDb,

                approvedActivity = ApprovedActivityFromDb,
                availableTags = tagsFromDb,
                showCreate = userClass.ShowCreate,
                showParticipate = userClass.ShowParticipate
            };

            return View(model);
        }

    }
}