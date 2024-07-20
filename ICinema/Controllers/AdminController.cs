using Azure.Core.Serialization;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public IActionResult SettingUpSendingService()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SettingUpSendingService(EmailSettingsVM emailSettingsVM)
        {
            if (!ModelState.IsValid)
            {
                return View(emailSettingsVM);
            }
            
            var emailSettings = new EmailSettings()
            {
                SmtpServer = "smtp.gmail.com",
                SmtpPort = 587,
                SmtpUsername = emailSettingsVM.SmtpUsername,
                SmtpPassword = emailSettingsVM.SmtpPassword,
                SenderEmail = emailSettingsVM.SenderEmail,
                SenderName = "ICinema",
            };
            var _isAdd = await _adminRepository.AddEmailSettings(emailSettings);
            if (_isAdd)
            {
                return RedirectToAction("Index", "Home");

            }
            ModelState.AddModelError(String.Empty, "Can't Add new EmailSettings");
            return View(emailSettingsVM);

        }
        public IActionResult CreateHall()
        {
            var hallVM= new HallVM();
           
            if (TempData["HallVM"] == null)
            {
                
                hallVM = new HallVM()
                {          
                    Seats = new List<List<Seat>>(),
                };
                hallVM.Seats.Add(new List<Seat>());
                return View(hallVM);

            }


            hallVM = JsonSerializer.Deserialize<HallVM>(TempData["HallVM"].ToString());
            return View(hallVM);
        }
        
    }
}
