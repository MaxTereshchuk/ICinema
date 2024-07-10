using System.Diagnostics.Eventing.Reader;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ICinema.Interfaces
{
    public interface IAppUserRepository
    {
        public Task<AppUser> GetByEmail(string email);
        public Task<bool> CheckPassword(AppUser user, LoginVM loginVM);
        public Task<Microsoft.AspNetCore.Identity.SignInResult> CheckPasswordSignIn(AppUser user, LoginVM loginVM);

	}
}
