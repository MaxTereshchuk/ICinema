using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDBContext>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    //await Seed.SeedUsersAndRolesAsync(app);
    Seed.SeedData(app);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//Ñlears authentication when starting the program
//app.Use(async (context, next) => 
//{
//	if (context.User.Identity.IsAuthenticated)
//	{
//		await context.SignOutAsync(IdentityConstants.ApplicationScheme);
//	}
//	await next();
//});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
