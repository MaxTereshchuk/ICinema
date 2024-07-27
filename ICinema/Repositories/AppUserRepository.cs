using System.Security.Claims;
using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

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

		public async Task<Microsoft.AspNetCore.Identity.IdentityResult> CreateUser(RegisterVM registerVM)
		{
			var newUser = new AppUser()
			{
				Email = registerVM.Email,
				UserName = registerVM.Email,


			};
			var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);
			if (newUserResponse.Succeeded)
			{
				if(registerVM.IsAdmin) 
					await _userManager.AddToRoleAsync(newUser, UserRoles.Admin);
				else
					await _userManager.AddToRoleAsync(newUser, UserRoles.User);

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
		public async Task<bool> SendEmailAsync(string email, string subject, string message)
		{		
			return await _emailSender.SendEmailAsync(email, subject,message);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
        {
			return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<AppUser> GetByIdAsync(string userId)
        {
			return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string code)
        {
			return await _userManager.ConfirmEmailAsync(user, code);
        }
		public async Task<IdentityResult> DeleteUserAsync(AppUser user)
		{
			return await _userManager.DeleteAsync(user);
		}

        public async Task<IdentityResult> UpdateUserAsync(AppUser user)
		{
			return await _userManager.UpdateAsync(user);
		}
		
    }
}
