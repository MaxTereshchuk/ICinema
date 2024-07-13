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
        public Task<bool> CheckPassword(AppUser user, LoginVM loginVM);
        public Task<Microsoft.AspNetCore.Identity.SignInResult> CheckPasswordSignIn(AppUser user, LoginVM loginVM);
        public Task<Microsoft.AspNetCore.Identity.IdentityResult> CreateUser(AppUser user, string password);
        public Task<bool> LogOut();
        public Task<Microsoft.AspNetCore.Identity.IdentityResult> EditPhoneNumber(AppUser user, string phoneNumber);
        public Task<Microsoft.AspNetCore.Identity.IdentityResult> EditCard(AppUser user, Card card);
        public Task<bool> SendEmail(string email);
        

	}
}
