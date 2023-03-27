// Create a new WebApplication builder with the given command line arguments.
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using KingdomWebApp.Data;
using KingdomWebApp.Helpers;
using KingdomWebApp.Interfaces;
using KingdomWebApp.Models;
using KingdomWebApp.Repository;
using KingdomWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddHttpContextAccessor();





// Add services to the container.
builder.Services.AddControllersWithViews();


// Register repository and service classes for dependency injection.
builder.Services.AddScoped<IGuildRepository, GuildRepository>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();

// Configure Cloudinary settings from the appsettings.json file.
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));


// Add and configure the ApplicationDbContext to use SQL Server.

builder.Configuration.AddEnvironmentVariables();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}





// Configure the ApplicationDbContext to use SQL Server with the correct connection string.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        var connectionString = builder.Configuration.GetConnectionString("DevelopmentConnection");
       options.UseSqlServer(connectionString);
    }
    else
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    }
});

// Add and configure Identity for AppUser and IdentityRole.
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add memory cache and session services.
builder.Services.AddMemoryCache();
builder.Services.AddSession();

// Configure authentication with cookies.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie();

// Build the application.
var app = builder.Build();

// Check if the seeddata argument is provided.
//if (args.Length == 1 && args[0].ToLower() == "seeddata")
//{
    // Create a service scope and seed data.
    using (var serviceScope = app.Services.CreateScope())
    {
        var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();
        var seed = new Seed(configuration);

        // Seed users and roles.
        await seed.SeedUsersAndRolesAsync(app);
        // Seed other data.
        seed.SeedData(app);
    }
//}

// Configure the HTTP request pipeline.
// If not in development mode, use a custom error handler and enable HSTS.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable HTTPS redirection, static files, and routing.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Enable authentication and authorization.
app.UseAuthentication();
app.UseAuthorization();

// Configure the default route for the application.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application.
app.Run();
