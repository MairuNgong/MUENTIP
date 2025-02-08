using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MUENTIP.Models;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
        if (result.Succeeded)
        {
            return Json(new { success = true });
        }
        return Json(new { success = false, message = "Invalid login attempt." });
    }

    [HttpPost]
    public async Task<IActionResult> Register(string registerEmail, string registerUsername, string registerPassword, string registerConfirmPassword)
    {
        // Check if the password and confirmation password match
        if (registerPassword != registerConfirmPassword)
        {
            return Json(new { success = false, message = "Passwords do not match." });
        }

        var normalizedEmail = registerEmail.ToUpper();
        // Manually query by normalized email to ensure a single result
        var existingUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);

        if (existingUser != null)
        {
            return Json(new { success = false, message = "This email is already registered." });
        }

        // Create the new user
        var user = new User { UserName = registerUsername, Email = registerEmail };
        var result = await _userManager.CreateAsync(user, registerPassword);

        // Check if registration was successful
        if (result.Succeeded)
        {
            return Json(new { success = true });
        }

        // If there was an error during registration, return the error message
        return Json(new { success = false, message = "Registration Failed", errors = result.Errors.Select(e => e.Description) });
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
