using ICinema.Interfaces;
using ICinema.Models;
using ICinema.Repositories;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ICinema.Controllers
{
	
	public class AppUserController : Controller
    {

		private readonly IAppUserRepository _appUserRepository;
		public AppUserController(IAppUserRepository appUserRepository)
		{

			_appUserRepository = appUserRepository;
		}
		public async Task<IActionResult> Index()
        {
			var user =await _appUserRepository.GetUser(User);
			if (user==null)
			{
				return RedirectToAction("Login");
			}

			UserPersonalProfileVM userPersonalProfileVM = new UserPersonalProfileVM()
			{
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				Balance = user.Balance,
				Card = user.Card,
				MyTickets = user.MyTickets
			};
            return View(userPersonalProfileVM);
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
		public IActionResult Register()
		{

			return View();
		}
		[HttpPost]
        
        public async Task<IActionResult> Register(RegisterVM registerVM)
		{
			if (!ModelState.IsValid)
			{
				return View(registerVM);
			}
			var user = await _appUserRepository.GetByEmail(registerVM.Email);
			if (user != null)
			{
				ModelState.AddModelError(string.Empty, "This Email has already registered");
				return View(registerVM);
			}

			
            
            var newUserResponse= await _appUserRepository.CreateUser(registerVM);
			if (newUserResponse.Succeeded)
			{
				return RedirectToAction("Index");
			}
			foreach (var error in newUserResponse.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return View(registerVM);
		}
		[HttpGet]
		public async Task<IActionResult> LogOut()
		{
			bool isLogOut = await _appUserRepository.LogOut();
			return RedirectToAction("Index", "Home");
		}
		public IActionResult EditPhoneNumber()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> EditPhoneNumber(EditPhoneNumberVM editPhoneNumberVM)
		{
			if (!ModelState.IsValid)
			{
				return View(editPhoneNumberVM);
			}
			var user = await _appUserRepository.GetUser(User);
			if (user != null) {
				var result = await _appUserRepository.EditPhoneNumber(user, editPhoneNumberVM.PhoneNumber);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return View(editPhoneNumberVM);
			}
			
			return View(editPhoneNumberVM);			
		}
		public IActionResult EditCard()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> EditCard(EditCardVM editCardVM)
		{
			//Here can be added some verify card Functionality
			//----------

			//----------
			if (!ModelState.IsValid)
			{
				return View(editCardVM);
			}
			var user = await _appUserRepository.GetUser(User);
			if (user != null)
			{
				Card newCard = new Card()
				{
					CardName = "MasterCard",
					CardHolderName= editCardVM.CardHolderName,
					CardNumber = editCardVM.CardNumber,
					ExpiryDate = editCardVM.ExpiryDate,
					CVV = editCardVM.CVV

				};

				var result = await _appUserRepository.EditCard(user, newCard);
				if (result.Succeeded)
				{
					return RedirectToAction("Index");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return View(editCardVM);
			}
			return View(editCardVM);
		}



	}
}
