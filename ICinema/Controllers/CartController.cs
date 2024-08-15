using ICinema.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ICinema.Models;
using Microsoft.AspNetCore.Authorization;
using ICinema.ViewModels;
namespace ICinema.Controllers
{
    
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartRepository cartRepository, IAppUserRepository appUserRepository, ILogger<CartController> logger)
        {
            _cartRepository = cartRepository;
            _appUserRepository = appUserRepository;
            _logger = logger;
        }
        public async  Task<IActionResult> Dashboard()
        {
            var user = await _appUserRepository.GetUser(User);
            if (user != null)
            {
                var cartVM = new CartVM()
                {
                    Screaning = user.Cart.Screaning == null ? null: user.Cart.Screaning,
                    Tickets = user.Cart.Tickets == null ? null: user.Cart.Tickets.ToList(),
                };  
                return View(cartVM);
            }
            return RedirectToAction("Login", "AppUser", new { returnUrl = "/Cart/DashBoard" });
        }
        [HttpPost]
        public async Task<IActionResult> AddTicket(string TicketJson)
        {
            var ticket = JsonSerializer.Deserialize<Ticket>(TicketJson);
            var user = await _appUserRepository.GetUser(User);
            if (user != null)
            {
                await _cartRepository.AddTicketAsync(user, ticket);
                
                return RedirectToAction("Dashboard");
                
                
            }
            return RedirectToAction("Login", "AppUser");
        }
        [HttpPost]
        public async Task<IActionResult> DeleteTicket(string TicketJson)
        {
            var ticket = JsonSerializer.Deserialize<Ticket>(TicketJson);
            var user = await _appUserRepository.GetUser(User);
            if (user != null)
            {
                await _cartRepository.DeleteTicketAsync(user, ticket);

                return RedirectToAction("Dashboard");


            }
            return RedirectToAction("Login", "AppUser");
        }
        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
           
            var user = await _appUserRepository.GetUser(User);
            if (user != null)
            {
                await _cartRepository.ClearCartAsync(user);

                return RedirectToAction("Dashboard");


            }
            return RedirectToAction("Login", "AppUser");
        }
        [HttpPost]
        public async Task<IActionResult> ClearCart(string TicketJson)
        {
            var ticket = JsonSerializer.Deserialize<Ticket>(TicketJson);
            var user = await _appUserRepository.GetUser(User);
            if (user != null)
            {
                await _cartRepository.ClearCartAsync(user, ticket);

                return RedirectToAction("Dashboard");


            }
            return RedirectToAction("Login", "AppUser");
        }

    }
}
