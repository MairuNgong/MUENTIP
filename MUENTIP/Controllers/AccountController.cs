using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MUENTIP.Models;

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
    public async Task<IActionResult> Login(string loginInput, string password)
    {
        var user = await _userManager.FindByNameAsync(loginInput);
        if (user == null)
        {
            user = await _userManager.FindByEmailAsync(loginInput);
        }


        if (user == null)
        {
            return Json(new { success = false, message = "Account doesn't exist." });
        }

        var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);
        if (result.Succeeded)
        {
            return Json(new { success = true, message = user.Id });
        }
        return Json(new { success = false, message = "Wrong username or password." });
    }


    [HttpPost]
    public async Task<IActionResult> Register(string registerEmail, string registerUsername, string registerPassword, string registerConfirmPassword)
    {
        
        if (registerPassword != registerConfirmPassword)
        {
            return Json(new { success = false, message = "Passwords do not match." });
        }

        var normalizedUserName = registerUsername.ToUpper();
        
        var existingUser = await _userManager.Users
            .FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);

        var normalizedEmail = registerEmail.ToUpper();

        var existingEmail = await _userManager.Users
            .FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
        if (existingEmail != null)
        {
            return Json(new { success = false, message = "this Email is already registered." });
        }
        else if (existingUser != null)
        {
            return Json(new { success = false, message = "This username is already taken." });
        }

        
        var user = new User { UserName = registerUsername, Email = registerEmail };
        var result = await _userManager.CreateAsync(user, registerPassword);

        
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return Json(new { success = true });
        }

        
        return Json(new { success = false, message = "Password must be at least 6 characters long and include uppercase, lowercase, a number, and a special character (., #, ?).", errors = result.Errors.Select(e => e.Description) });
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
