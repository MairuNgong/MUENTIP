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
using System.Net.Mail;
using System.Net;
using System.Net.Mime;

public class SelectController : Controller
{
    private readonly ApplicationDBContext _context;
    private readonly EmailService _emailService;

    public SelectController(ApplicationDBContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
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

        if (member == null)
        {
            return NotFound("No members found for this activity.");
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
            return BadRequest("Invalid activity ID provided.");
        }

        var activity = await _context.Activities.FirstOrDefaultAsync(a => a.ActivityId == activityIds);

        if (activity == null)
        {
            return NotFound();
        }


        activity.DeadlineDateTime = DateTime.UtcNow.AddDays(-1);  
        _context.Activities.Update(activity);



        

        var existingParticipations = await _context.ParticipateIn
            .Where(p => p.ActivityId == activityIds && p.UserId == user_id)
            .ToListAsync();

        if (existingParticipations.Any())
        {
            _context.ParticipateIn.RemoveRange(existingParticipations); 
        }


        var Participate = new ParticipateIn
        {
            ActivityId = activityIds,
            UserId = user_id,
            AppliedDate = DateTime.UtcNow
        };

        _context.ParticipateIn.Add(Participate); 
        await _context.SaveChangesAsync();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == user_id);
        if (user != null)
        {
           
            string subject = $@"🎉 Congratulations! You've Been Selected for the {activity.Title} Event!";


            string body = $@"<p>Dear {user.UserName},</p>

                            <p style='font-size:18px;'><strong>🎊 A Huge Congratulations! 🎊</strong></p>

                            <p>You have been selected to participate in the <strong>""{activity.Title}""</strong> 
                            event, where fun and entertainment await you! ✨</p>

                            <p><strong>Get ready and join us for an exciting experience! 😆🎯</strong></p>

                            <p>Best regards,<br>
                            [MUENTIP]</p>";



            await _emailService.SendEmailAsync(user.Email, subject, body);
        }

        return Ok(new { message = "User successfully added to the activity." });
    }
}

public class EmailService
{
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;
    private readonly string _fromEmail;

    public EmailService()
    {
        _smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? "default_host";
        _smtpPort = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out int port) ? port : 587;
        _smtpUser = Environment.GetEnvironmentVariable("SMTP_USERNAME") ?? "default_user";
        _smtpPass = Environment.GetEnvironmentVariable("SMTP_PASSWORD") ?? "default_password";
        _fromEmail = Environment.GetEnvironmentVariable("SMTP_FROM_EMAIL") ?? "default_from";
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient(_smtpHost)
        {
            Port = _smtpPort,
            Credentials = new NetworkCredential(_smtpUser, _smtpPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_fromEmail),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }
}