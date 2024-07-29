using ICinema.Models;

namespace ICinema.Interfaces
{
    public interface ICartRepository
    {
        public Task ClearCartAsync(AppUser user);
        /// <summary>
        /// This method is the same as ClearCartAsync(AppUser user), but takes the second parameter ticket, which will be added to the cart, after clearing
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ticket">Ticket, which will be added after cart clearing</param>
        /// <returns></returns>
        public Task ClearCartAsync(AppUser user, Ticket ticket);
        public Task AddTicketAsync(AppUser user, Ticket ticket);
        public Task DeleteTicketAsync(AppUser user, Ticket ticket);


    }
}
