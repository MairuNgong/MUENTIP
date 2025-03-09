using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.ViewModels;
using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;

namespace MUENTIP.Controllers
{
    public class MyActivityController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;

        public MyActivityController(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User); // ดึง user ที่ login อยู่

            if (user == null)
            {
                return NotFound(); ; // กลับไปหน้า login ถ้ายังไม่ได้ login
            }

            var userClass = await _context.Users
            .Where(u => u.Id == user.Id)
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

                CreatedActivities = u.CreatedActivities.OrderByDescending(a => a.ActivityId).Select(a => new
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

                Applications = u.Applications.OrderByDescending(a => a.ActivityId).Select(apply => new
                {
                    Activity = apply.Activity != null ? new
                    {
                        apply.Activity.ActivityId,
                        apply.Activity.Title,
                        OwnerName = apply.Activity.User.UserName,
                        apply.Activity.Location,
                        apply.Activity.StartDateTime,
                        apply.Activity.EndDateTime,
                        apply.Activity.DeadlineDateTime,
                        apply.Activity.ApplyMax,
                        ApplyCount = apply.Activity.Applications.Count(),
                        Tags = apply.Activity.ActivityTags.Select(at => at.Tag.TagName).ToList()
                    } : null
                }).ToList(),

                Participations = u.Participations.OrderByDescending(a => a.ActivityId).Select(p => new
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

                        NonApprovedActivityFromDb = userClass.Applications
                            .Where(apply => apply.Activity != null)
                            .Select(apply => new ActivityCardViewModel
                            {
                            ActivityId = apply.Activity.ActivityId,
                            Title = apply.Activity.Title,
                            Owner = apply.Activity.OwnerName,
                            Location = apply.Activity.Location,
                            StartDateTime = apply.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                            EndDateTime = apply.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                            DeadlineDateTime = apply.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                            ApplyMax = apply.Activity.ApplyMax,
                            ApplyCount = apply.Activity.ApplyCount,
                            TagsList = apply.Activity.Tags
                            })
                            .ToList();
                    }
                    else
                    {
                        return NotFound();
                    }

            // ดึงแท็กจากฐานข้อมูล
            var tagsFromDb = await _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToListAsync();

            // สร้าง ViewModel
            var model = new MyActivityViewModel
            {
                userId = int.TryParse(user.Id, out int id) ? id : 0, 
                createdActivity = CreatedActivityFromDb,
                nonApproveActivity = NonApprovedActivityFromDb,
                approvedActivity = ApprovedActivityFromDb,
                filterTags = tagsFromDb
            };

            return View(model);
        }
    }
}
