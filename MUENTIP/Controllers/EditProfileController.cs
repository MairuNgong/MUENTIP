using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.ViewModels;
using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MUENTIP.Controllers
{
    public class EditProfileController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly Cloudinary _cloudinary;

        public EditProfileController(ApplicationDBContext context, UserManager<User> userManager, Cloudinary cloudinary)
        {
            _context = context;
            _userManager = userManager;
            _cloudinary = cloudinary;   

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var userClass = await _context.Users
                .Include(u => u.InterestedTags)
                .ThenInclude(it => it.Tag)
                .FirstOrDefaultAsync(u => u.Id == user.Id);

            var tagsFromDb = await _context.Tags
                .Select(t => new TagFilterViewModel { TagName = t.TagName })
                .ToListAsync();

            var model = new MyProfileViewModel
            {
                UserName = user.UserName,
                ProfileImageLink = user.ProfileImageLink,
                Email = user.Email,
                Info = user.Info,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Education = user.Education,
                Address = user.Address,
                InterestedTags = user.InterestedTags?.Select(it => it.TagName).ToList() ?? new List<string>(),
                availableTags = tagsFromDb,
                showCreate = user.ShowCreate,
                showParticipate = user.ShowParticipate
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Image(IFormFile file)
        {   
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded" });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = "uploads"
            };

            

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            var imageUrl = uploadResult.SecureUrl.ToString();

            return Ok(new { imageUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MyProfileViewModel model)
        {   

            var user = await _userManager.GetUserAsync(User);

            

            if (user != null)
            {
               
                if (model.UserName != user.UserName && _context.Users.Any(u => u.UserName == model.UserName))
                {
                    return Json(new { success = false, message = "UserName already taken" });
                }

                if (model.Email != user.Email && _context.Users.Any(u => u.Email == model.Email))
                {
                    return Json(new { success = false, message = "Email already in used" });
                }
                if (model.BirthDate != null)
                {
                    if (model.BirthDate > DateOnly.FromDateTime(DateTime.Now))
                    {
                        return Json(new { success = false, message = "Birthdate is invalid." });
                    }
                }

                user.UserName = model.UserName;
                user.NormalizedUserName = model.UserName.ToUpper();
                user.Email = model.Email;
                user.NormalizedEmail = model.Email.ToUpper();
                user.Info = model.Info;
                user.BirthDate = model.BirthDate;
                user.Gender = model.Gender;
                user.Education = model.Education;
                user.Address = model.Address;
                user.ShowCreate = model.showCreate;
                user.ShowParticipate = model.showParticipate;

                if (!string.IsNullOrEmpty(model.ProfileImageLink))
                {
                    if (!string.IsNullOrEmpty(user.ProfileImageLink))
                    {
                        var oldImageUrl = new Uri(user.ProfileImageLink);
                        var fullImageUrl = oldImageUrl.AbsolutePath;
                        var imagePath = fullImageUrl.Substring(fullImageUrl.IndexOf("uploads"));
                        var imagePathWithoutExtension = Path.GetFileNameWithoutExtension(imagePath);
                        var deleteParams = new DelResParams()
                        {
                            PublicIds = new List<string> { $"uploads/{imagePathWithoutExtension}" },
                            Type = "upload",
                            ResourceType = ResourceType.Image
                        };

                        var result = _cloudinary.DeleteResources(deleteParams);
                    }
                        user.ProfileImageLink = model.ProfileImageLink;

                }

                
                var JSONtags = model.InterestedTags;  
                List<string> tags = JsonConvert.DeserializeObject<List<string>>(JSONtags[0]);
                var currentTags = await _context.InterestIn
                    .Where(interest => interest.UserId == user.Id)
                    .ToListAsync();

                
                _context.InterestIn.RemoveRange(currentTags);

                
                foreach (var tag in tags)
                {
                    
                    var tagExists = await _context.Tags.AnyAsync(t => t.TagName == tag);
                    if (!tagExists)
                    {
                        return Json(new { success = false, message = $"Tag '{tag}' does not exist." });
                    }

                    
                    var InterestIn = new InterestIn
                    {
                        UserId = user.Id,
                        TagName = tag

                    };

                    _context.InterestIn.Add(InterestIn);
                }

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = $"{user.Id}"});
            }

            return Json(new { success = false, message = "Unorthorizes" });
        }

    }
}
