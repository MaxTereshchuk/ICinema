using ICinema.Models;

namespace ICinema.Interfaces
{
    public interface ICartRepository
    {
        public Task ClearCartAsync(int cartId);
        public Task ClearCartAsync(int cartId, Ticket ticket);
        public Task AddTicketAsync(AppUser user, Ticket ticket);
        public Task DeleteTicket(int ticketId);


    }
}
