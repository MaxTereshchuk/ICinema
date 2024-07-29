using CloudinaryDotNet.Actions;
using ICinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ICinema.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
			//migrationBuilder.Sql(@"
			//         INSERT INTO AspNetRoles (Id, Name, NormalizedName)
			//         VALUES 
			//         ('1', 'Admin', 'ADMIN'),
			//         ('2', 'User', 'USER');

			//     ");
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDBContext>();
				var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();


				context.Database.EnsureCreated();
				if (!context.Roles.Any())
				{
					roleManager.CreateAsync(new IdentityRole(UserRoles.Admin)).Wait();
					roleManager.CreateAsync(new IdentityRole(UserRoles.User)).Wait();
				}
			}
		}
	
    }
}
