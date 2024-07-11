using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace ICinema.Repositories
{
    public class AppUserRepository: IAppUserRepository
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly AppDBContext _appDBContext;
		public AppUserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDBContext appDBContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_appDBContext = appDBContext;
		}

		public async Task<AppUser> GetByEmail(string email)
		{
			return await _userManager.FindByEmailAsync(email);
		}
		public async Task<bool> CheckPassword(AppUser user, LoginVM loginVM) 
		{
			return await _userManager.CheckPasswordAsync(user, loginVM.Password);
		}
		public async Task<Microsoft.AspNetCore.Identity.SignInResult> CheckPasswordSignIn(AppUser user, LoginVM loginVM)
		{
			return await _signInManager.PasswordSignInAsync(user, loginVM.Password, false,false);
		}

		public async Task<Microsoft.AspNetCore.Identity.IdentityResult> CreateUser(AppUser user, string password)
		{
			var newUserResponse = await _userManager.CreateAsync(user, password);
			if (newUserResponse.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, UserRoles.User);
				
			}
			return newUserResponse;
		}
		
	}
}
