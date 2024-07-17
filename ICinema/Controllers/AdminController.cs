using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ICinema.Controllers
{
	[Authorize(Policy = "AdminOnly")]
	public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        public AdminController(IAdminRepository adminRepository) 
        {
            _adminRepository = adminRepository;
        }
        public IActionResult CreateTicket() 
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketVM createTicketVM)
        {
            if (!ModelState.IsValid) 
                return View(createTicketVM);

            var ticket = new Ticket()
            {
                MovieName = createTicketVM.MovieName,
                Date = createTicketVM.Date,
                ScreaningId = createTicketVM.ScreaningId,
                RowNumber = createTicketVM.RowNumber,
                SeatNumber = createTicketVM.SeatNumber,
                Price = createTicketVM.Price,
                ImageUrl = createTicketVM.ImageUrl,
            };
            bool result = await _adminRepository.CreateTicket(ticket);
            if (result)
            {
                return RedirectToAction("Index", "Home");
                
            }
            return View(createTicketVM);
        }

    }
}
