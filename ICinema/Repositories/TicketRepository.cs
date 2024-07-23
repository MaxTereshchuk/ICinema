using ICinema.Interfaces;
using ICinema.Models;

namespace ICinema.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Task<Ticket> GetTicketByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task PurchaseTicket(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
