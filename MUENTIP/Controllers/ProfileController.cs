﻿using Microsoft.AspNetCore.Mvc;
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
                .Include(u => u.CreatedActivities)
                    .ThenInclude(a => a.Applications)
                .Include(u => u.CreatedActivities)
                    .ThenInclude(a => a.ActivityTags)
                        .ThenInclude(at => at.Tag)
                .Include(u => u.Applications)
                    .ThenInclude(apply => apply.Activity)
                        .ThenInclude(a => a.User)
                .Include(u => u.Applications)
                    .ThenInclude(apply => apply.Activity)
                        .ThenInclude(a => a.ActivityTags)
                            .ThenInclude(at => at.Tag)
                .Include(u => u.Participations)
                    .ThenInclude(p => p.Activity)
                        .ThenInclude(a => a.User)
                .Include(u => u.InterestedTags)
                    .ThenInclude(it => it.Tag)
                .FirstOrDefaultAsync(u => u.Id == id);

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
                        Owner = t.User.UserName,
                        Location = t.Location,
                        StartDateTime = t.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        EndDateTime = t.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        DeadlineDateTime = t.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                        ApplyMax = t.ApplyMax,
                        ApplyCount = t.Applications.Count(),
                        TagsList = t.ActivityTags?.Select(at => at.Tag.TagName).ToList() ?? new List<string>()
                    })
                    .ToList();

        

                ApprovedActivityFromDb = userClass.Participations
                    .Where(p => p.Activity != null)
                    .Select(p => new ActivityCardViewModel
                    {
                        ActivityId = p.Activity.ActivityId,
                        Title = p.Activity.Title,
                        Owner = p.Activity.User.UserName ?? "Unknown",
                        Location = p.Activity.Location,
                        StartDateTime = p.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm"),
                        EndDateTime = p.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm"),
                        DeadlineDateTime = p.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm"),
                        ApplyMax = p.Activity.ApplyMax,
                        ApplyCount = p.Activity.Applications.Count(),
                        TagsList = p.Activity.ActivityTags?.Select(at => at.Tag.TagName).ToList() ?? new List<string>()
                    })
                    .ToList();
            }
            else{
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
                InterestedTags = userClass.InterestedTags?.Select(it => it.TagName).ToList() ?? new List<string>(),
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
