using ICinema.Models;
namespace ICinema.Interfaces
{
    public interface ITicketRepository
    {
        public Task<Ticket> GetTicketByIdAsync(int id);
        public Task PurchaseTicket(Ticket ticket);

    }
}
