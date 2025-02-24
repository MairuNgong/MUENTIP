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

builder.Services.AddDbContext<ApplicationDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);



builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

Env.Load();

var cloudinaryAccount = new Account(
    Env.GetString("CLOUDINARY_CLOUD_NAME"),
    Env.GetString("CLOUDINARY_API_KEY"),
    Env.GetString("CLOUDINARY_API_SECRET")
);

var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
