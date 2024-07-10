using ICinema.Interfaces;
using ICinema.Models;
using ICinema.Repositories;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ICinema.Controllers
{
    public class AppUserController : Controller
    {

		private readonly IAppUserRepository _appUserRepository;
		public AppUserController(IAppUserRepository appUserRepository)
		{

			_appUserRepository = appUserRepository;
		}
		public IActionResult Index()
        {
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login");
			}
            return View();
        }
		public IActionResult Login()
		{
			
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM loginVM)
		{
			if (!ModelState.IsValid)
			{
				return View(loginVM);
			}
			var user = await _appUserRepository.GetByEmail(loginVM.Email);
			if (user != null) 
			{
				bool isPasswordCorrect = await _appUserRepository.CheckPassword(user, loginVM);
				if (isPasswordCorrect) 
				{
					var result =  await _appUserRepository.CheckPasswordSignIn(user, loginVM);
					if (result.Succeeded)
					{
						return RedirectToAction("Index");
					}
				}
				ModelState.AddModelError(string.Empty, "Incorrect password.");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "User not found.");
			}
			return View(loginVM);

		}
	}
}
