using ICinema.Interfaces;
using ICinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace ICinema.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;
        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public IActionResult DashBoard(Ticket ticket)
        {
            return View(ticket);
        }
        


    }
}
