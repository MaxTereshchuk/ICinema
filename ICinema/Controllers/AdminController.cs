using Azure.Core.Serialization;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
                
                ScreaningId = createTicketVM.ScreaningId,
                RowNumber = createTicketVM.RowNumber,
                SeatNumber = createTicketVM.SeatNumber,
                Price = createTicketVM.Price,
                
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
        [HttpPost]
        public IActionResult CreateHall(string hallVMJson)
        {
            var hallVM = new HallVM();

            if (hallVMJson == null)
            {

                hallVM = new HallVM()
                {
                    Seats = new List<List<Seat>>(),
                };
                hallVM.Seats.Add(new List<Seat>());
                return View(hallVM);

            }


            hallVM = JsonSerializer.Deserialize<HallVM>(hallVMJson);
            return View(hallVM);
        }
        public IActionResult HallsPage()
        {
            
            if (TempData["hallsVMJson"]==null)
            {
                TempData["ActionToRedirect"] = "HallsPage";
				TempData["ControllerToRedirect"] = "Admin";
				return RedirectToAction("GetAllHalls", "Hall");
            }
            var hallsVM = JsonSerializer.Deserialize<ICollection<HallVM>>(TempData["hallsVMJson"].ToString());
            return View(hallsVM);
        }
        public IActionResult CreateFilm()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateFilm(CreateFilmVM createFirlmVM)
        {
            if (!ModelState.IsValid)
                return View(createFirlmVM);

            var film = new Film()
            {
                Title = createFirlmVM.Title,
                Image = createFirlmVM.Image,
            };

            await _adminRepository.AddFilmAsync(film);
            return RedirectToAction("CreateSchedule", film);
        }
        public IActionResult CreateSchedule(Film film)
        {
            CreateScheduleVM createScheduleVM = new CreateScheduleVM()
            {
                Film = film,
                FilmId=film.Id,
            };
            return View(createScheduleVM);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSchedule(CreateScheduleVM createScheduleVM)
        {
            if (!ModelState.IsValid)
                return View(createScheduleVM);
            var schedule = new Schedule()
            {
                Day = createScheduleVM.Day,
                Film = createScheduleVM.Film,
                FilmId = createScheduleVM.FilmId,
                
            };
            
            TempData["NumberOfScreanings"]=createScheduleVM.NumberOfScreanings;
            await _adminRepository.AddScheduleAsync(schedule);
            return RedirectToAction("CreateScreaning", schedule);

        }
        public IActionResult CreateScreaning(Schedule schedule)
        {
            if (TempData["hallsVMJson"] == null)
            {
				TempData["ActionToRedirect"] = "CreateScreaning";
				TempData["ControllerToRedirect"] = "Admin";
				return RedirectToAction("GetAllHalls", "Hall");
			}
                
            List<CreateScreaningVM> screanings = new List<CreateScreaningVM>();
            var halls = JsonSerializer.Deserialize<ICollection<HallVM>>(TempData["hallsVMJson"].ToString());

			if (TempData["NumberOfScreanungs"] != null)
            {
                int count = int.Parse(TempData["NumberOfScreanings"].ToString());
                for (int i=0; i<count; i++)
                {
                    CreateScreaningVM createScreaningVM = new CreateScreaningVM()
                    {
                        Day = schedule.Day,
                        Schedule = schedule,
                        ScheduleId = schedule.Id,
						AvailableHalls = halls.Select(h => new SelectListItem
						{
							Value = h.Id.ToString(),
							
						}).ToList(),
					};
                    
                    screanings.Add(createScreaningVM);
                }
                
            }
            return View(screanings);
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateScreaning(CreateScreaningVM createScreaningVM)
        {

            if (TempData["Hall"] ==null)
            {
                TempData["ActionToRedirect"] = "CreateScreaning";
                TempData["ControllerToRedirect"] = "Admin";
                return RedirectToAction("GetHallById", "Hall");

            }
            if (!ModelState.IsValid)
                return View(createScreaningVM);

            Screaning screaning = new Screaning()
            {
                Day = createScreaningVM.Day,
                Schedule = createScreaningVM.Schedule,
                ScheduleId = createScreaningVM.ScheduleId,
                Hall = JsonSerializer.Deserialize<Hall>(TempData["Hall"].ToString()),
                HallId = createScreaningVM.HallId,
            };
            await _adminRepository.AddScreaningAsync(screaning);
            await _adminRepository.GenerateTicketsAsync(screaning);
            
            return RedirectToAction("Index", "Home");
        }


    }
}
