using System.Security.Claims;
using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ICinema.Repositories
{
    public class AppUserRepository: IAppUserRepository
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly AppDBContext _appDBContext;
		private readonly IEmailSender _emailSender;
		public AppUserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDBContext appDBContext, IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_appDBContext = appDBContext;
			_emailSender = emailSender;
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
		public async Task<bool> LogOut()
		{

			try
			{
				await _signInManager.SignOutAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<AppUser> GetUser(ClaimsPrincipal User)
		{
			 var user = await _userManager.GetUserAsync(User);
			if (user == null)
				return user;
			
			return await _appDBContext.AppUsers.Include(u=>u.Card).FirstOrDefaultAsync(u => u.Id == user.Id);
		}

        public async Task<Microsoft.AspNetCore.Identity.IdentityResult> EditPhoneNumber(AppUser user, string phoneNumber)
        {
            user.PhoneNumber = phoneNumber;
            var result = await _userManager.UpdateAsync(user);
            return result;

        }

        public async Task<Microsoft.AspNetCore.Identity.IdentityResult> EditCard(AppUser user, Card card)
        {
            user.Card = card;
			var result = await _userManager.UpdateAsync(user);
			return result;
        }
		public async Task<bool> SendEmail(string email)
		{		
			return await _emailSender.SendEmailAsync(email, "Confirm your email", "Hello");
        }
		
	}
}
