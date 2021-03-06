using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataLayer.Data;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using DataLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Datalayer.UnitOfWork;
using DataLayer.UnitOfWork;
using BusinessLogicLayer.BackgroundTaskQueue;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// IHost host = Host.CreateDefaultBuilder(args)
//     .ConfigureServices((hostContext, services) =>
//     {
//         services.AddSingleton<MonitorLoop>();
//         services.AddHostedService<QueuedHostedService>();
//         services.AddSingleton<IBackgroundTaskQueue>(ctx =>
//         {
//             if (!int.TryParse(hostContext.Configuration["QueueCapacity"], out var queueCapacity))
//                 queueCapacity = 100;
//             return new BackgroundTaskQueue(queueCapacity);
//         });
//     })
//     .Build();

 builder.Services.AddDbContext<MuseumManagementContext>(options =>
     options.UseSqlite(builder.Configuration.GetConnectionString("MuseumManagementContext")));

// builder.Services.AddDbContext<MuseumManagementContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("MuseumManagementContext")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddEntityFrameworkStores<MuseumManagementContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
    

// Inject UnitOfWork in controllers
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddRazorPages();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
    SeedData.CreateRolesAsync(services);
    SeedData.CreateAdminAsync(services);
    // var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
    // monitorLoop.StartMonitorLoop();
   
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Authentication & Authorization


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Catalog}/{action=Index}/{id?}"
    
    );

app.MapRazorPages();

app.Run();
