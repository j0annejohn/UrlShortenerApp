using Microsoft.EntityFrameworkCore;
using UrlShortenerApp.Data; // Add this to make sure it finds your Data folder

var builder = WebApplication.CreateBuilder(args);

// --- ALL SERVICES GO HERE (Before builder.Build) ---
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- SETUP ENDS ---
var app = builder.Build();

// --- THE APP STARTS RUNNING HERE ---
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();