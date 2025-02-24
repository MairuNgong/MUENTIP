using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MUENTIP.Data;
using MUENTIP.Models;
using MUENTIP.ViewModels;
using System.Threading.Tasks;

public class TestUploadProController : Controller
{
    private readonly Cloudinary _cloudinary;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDBContext _context;

    public TestUploadProController(Cloudinary cloudinary, UserManager<User> userManager, ApplicationDBContext context)
    {
        _cloudinary = cloudinary;
        _userManager = userManager;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = "No file uploaded" });
        }

        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "uploads"
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        var imageUrl = uploadResult.SecureUrl.ToString();

        var user = await _userManager.GetUserAsync(User);
        if (user != null)
        {
            user.ProfileImageLink = imageUrl;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        return Ok(new { imageUrl });
    }
}

