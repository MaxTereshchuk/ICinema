using ICinema.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ICinema.Models;
namespace ICinema.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IAppUserRepository _appUserRepository;
        public CartController(ICartRepository cartRepository, IAppUserRepository appUserRepository)
        {
            _cartRepository = cartRepository;
            _appUserRepository = appUserRepository;
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

    }
}
