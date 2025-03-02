using Microsoft.AspNetCore.Identity;
using MUENTIP.Models;
using Microsoft.EntityFrameworkCore;
using MUENTIP.Data;
using CloudinaryDotNet;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication()
    .AddCookie();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

Env.Load(); // Load environment variables

// Register Cloudinary as a singleton service
var cloudinaryAccount = new Account(
    Env.GetString("CLOUDINARY_CLOUD_NAME"),
    Env.GetString("CLOUDINARY_API_KEY"),
    Env.GetString("CLOUDINARY_API_SECRET")
);
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary); // Register Cloudinary

// Register EmailService as a transient service
builder.Services.AddTransient<EmailService>(); // Register EmailService

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Detailed error pages in development
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=SearchPage}/{action=Index}/{id?}");

app.Run();
