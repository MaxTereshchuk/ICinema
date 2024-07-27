using ICinema.Data;
using ICinema.Interfaces;
using System.Text.Json;
using ICinema.Models;
using ICinema.Controllers;
namespace ICinema.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDBContext _appDBContext;
        private readonly IAppUserRepository _appUserRepository;
        private readonly ILogger<ICartRepository> _logger;
        public CartRepository(AppDBContext appDBContext, IAppUserRepository appUserRepository, ILogger<ICartRepository> logger)
        {
            _appDBContext = appDBContext;
            _appUserRepository = appUserRepository;
            _logger = logger;
        }

        public async Task AddTicketAsync(AppUser user, Ticket ticket)
        {
            user.Cart.Tickets.Add(ticket);
            user.Cart.Screaning=ticket.Screaning;
            user.Cart.ScreaningId=ticket.ScreaningId;
            var result= await _appUserRepository.UpdateUserAsync(user);
            if (!result.Succeeded)
            {
                _logger.LogError("Error occurred while updating user. Errors: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                throw new Exception("Failed to update user.");
            }
            
            
        }

        public Task ClearCartAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        public Task ClearCartAsync(int cartId, Ticket ticket)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTicket(int ticketId)
        {
            throw new NotImplementedException();
        }
    }
}
