using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ICinema.Interfaces
{
    public interface IAppUserRepository
    {
        public Task<AppUser> GetUser(ClaimsPrincipal user);
        public Task<AppUser> GetByEmail(string email);
        public Task<AppUser> GetByIdAsync(string userId);
        public Task<bool> CheckPassword(AppUser user, LoginVM loginVM);
        public Task<Microsoft.AspNetCore.Identity.SignInResult> CheckPasswordSignIn(AppUser user, LoginVM loginVM);
        public Task<Microsoft.AspNetCore.Identity.IdentityResult> CreateUser(RegisterVM registerVM);
        public Task<bool> LogOut();
        public Task<Microsoft.AspNetCore.Identity.IdentityResult> EditPhoneNumber(AppUser user, string phoneNumber);
        public Task<Microsoft.AspNetCore.Identity.IdentityResult> EditCard(AppUser user, Card card);
        public Task<bool> SendEmailAsync(string email, string subject, string message);
        public Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);

        public Task<Microsoft.AspNetCore.Identity.IdentityResult> ConfirmEmailAsync(AppUser user, string code);

        public Task<Microsoft.AspNetCore.Identity.IdentityResult> DeleteUserAsync(AppUser user);
    }
}
