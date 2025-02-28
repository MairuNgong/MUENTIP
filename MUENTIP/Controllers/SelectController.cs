using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;


public class SelectController : Controller
{
    private readonly ApplicationDBContext _context;

    public SelectController(ApplicationDBContext context)
    {
        _context = context;
    }


    public IActionResult Index(int id)
    {
        var Activity = _context.Activities.Include(a => a.Applications).FirstOrDefault(u => u.ActivityId == id);

        if (Activity == null)
        {
            return NotFound();
        }

        var owner_id = Activity.UserId;

        var user = _context.Users.FirstOrDefault(u => u.Id == owner_id);

        var member = Activity.Applications.ToList();

        if (member == null) {
            return Ok("hell");
        }

        var userIds = member.Select(a => a.UserId).Distinct().ToList();
        var users = _context.Users.Where(u => userIds.Contains(u.Id)).ToList();

        var membersWithUsernames = users.Select(a => new UserCardViewModel
        {
            userId = a.Id,
            userName = a.UserName,
            email = a.Email,
            userImgLink = a.ProfileImageLink
        }).ToList();


        // Map directly to the view model
        var model = new SelectViewModel
        {
            ownerId = owner_id,
            ownerImgLink = user.ProfileImageLink,
            applyMax = Activity.ApplyMax,
            email = user.Id,
            Appliers = membersWithUsernames,
            activity_id = Activity.ActivityId
        }; 


        return View(model);


    }
    [HttpPost]
    public async Task<IActionResult> select_create(string activity_id, string user_id)
    {
        if (!int.TryParse(activity_id, out int activityIds))
        {
            return BadRequest("Invalid activity ID.");
        }
        var activity = await _context.Activities.FirstOrDefaultAsync(a => a.ActivityId == activityIds);

        if (activity == null)
        {
            return NotFound();
        }

        activity.DeadlineDateTime = DateTime.UtcNow.AddDays(-1);

        _context.Activities.Update(activity);

        var Participate = new ParticipateIn
        {
            ActivityId = activityIds,
            UserId = user_id,
            AppliedDate = DateTime.UtcNow
        };

        _context.ParticipateIn.Add(Participate); // Use the correct DbSet
        await _context.SaveChangesAsync();

        return Ok(new { message = "User successfully added to the activitys." });
    }


}



