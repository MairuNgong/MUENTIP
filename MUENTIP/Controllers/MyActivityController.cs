﻿using Microsoft.AspNetCore.Mvc;
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
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var userId = user.Id;

            
            var createdActivities = await _context.Activities
                .AsNoTracking()
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.ActivityId)
                .Select(a => new ActivityCardViewModel
                {
                    ActivityId = a.ActivityId,
                    Title = a.Title,
                    Owner = a.User.UserName,
                    Location = a.Location,
                    StartDateTime = a.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    EndDateTime = a.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    DeadlineDateTime = a.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ApplyMax = a.ApplyMax,
                    ApplyCount = a.Applications.Count(),
                    TagsList = a.ActivityTags.Select(at => at.Tag.TagName).ToList()
                })
                .ToListAsync();

            
            var applications = await _context.ApplyOn
                .AsNoTracking()
                .Where(a => a.UserId == userId && a.Activity != null)
                .OrderByDescending(a => a.Activity.ActivityId)
                .Select(apply => new ActivityCardViewModel
                {
                    ActivityId = apply.Activity.ActivityId,
                    Title = apply.Activity.Title,
                    Owner = apply.Activity.User.UserName,
                    Location = apply.Activity.Location,
                    StartDateTime = apply.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    EndDateTime = apply.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    DeadlineDateTime = apply.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ApplyMax = apply.Activity.ApplyMax,
                    ApplyCount = apply.Activity.Applications.Count(),
                    TagsList = apply.Activity.ActivityTags.Select(at => at.Tag.TagName).ToList()
                })
                .ToListAsync();

            
            var participations = await _context.ParticipateIn
                .AsNoTracking()
                .Where(p => p.UserId == userId && p.Activity != null)
                .OrderByDescending(p => p.Activity.ActivityId)
                .Select(p => new ActivityCardViewModel
                {
                    ActivityId = p.Activity.ActivityId,
                    Title = p.Activity.Title,
                    Owner = p.Activity.User.UserName,
                    Location = p.Activity.Location,
                    StartDateTime = p.Activity.StartDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    EndDateTime = p.Activity.EndDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    DeadlineDateTime = p.Activity.DeadlineDateTime.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ApplyMax = p.Activity.ApplyMax,
                    ApplyCount = p.Activity.Applications.Count(),
                    TagsList = p.Activity.ActivityTags.Select(at => at.Tag.TagName).ToList()
                })
                .ToListAsync();

            
             var nonApprovedActivities = applications
                .Where(apply => !participations.Any(p => p.ActivityId == apply.ActivityId)) 
                .ToList();

            var tags = await _context.Tags
                .AsNoTracking()
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToListAsync();

            var model = new MyActivityViewModel
            {
                userId = int.TryParse(userId, out int id) ? id : 0,
                createdActivity = createdActivities,
                nonApproveActivity = nonApprovedActivities,
                approvedActivity = participations,
                filterTags = tags
            };

            return View(model);
        }

    }
}