using Azure.Core.Serialization;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace ICinema.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IFilmsRepository _filmsRepository;

        public AdminController(IAdminRepository adminRepository, IHallRepository hallRepository, IFilmsRepository filmsRepository)
        {
            _adminRepository = adminRepository;
            _hallRepository = hallRepository;
            _filmsRepository = filmsRepository;

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
            var hallVM = new HallVM();

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

            if (TempData["hallsVMJson"] == null)
            {
                TempData["ActionToRedirect"] = "HallsPage";
                TempData["ControllerToRedirect"] = "Admin";
                return RedirectToAction("GetAllHalls", "Hall");
            }
            var hallsVM = JsonSerializer.Deserialize<ICollection<HallVM>>(TempData["hallsVMJson"].ToString());
            return View(hallsVM);
        }
        [HttpGet]
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
            
            return RedirectToAction("ListOfFilms");
        }
        [HttpGet]
        public IActionResult CreateSchedule(string filmJson)
        {
            
            var film = JsonSerializer.Deserialize<Film>(filmJson);
            
            CreateScheduleVM createScheduleVM = new CreateScheduleVM()
            {
                Day = DateTime.Now,
                Film = film,
                FilmId = film.Id,
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


            await _adminRepository.AddScheduleAsync(schedule);
            
            return RedirectToAction("ListOfFilms");

        }
        [HttpGet]
        public async Task<IActionResult> CreateScreaning(string scheduleJson)
        {
            var schedule = JsonSerializer.Deserialize<Schedule>(scheduleJson);

            
            var hallsVM= await _hallRepository.GetAllHallsAsync();         
            CreateScreaningVM createScreaningVM = new CreateScreaningVM()
            {
                Day = schedule.Day,
                Schedule = schedule,
                ScheduleId = schedule.Id,
				AvailableHalls = hallsVM.Select(h => new SelectListItem
				{
					Value = h.Id.ToString(),
                    Text=$"Hall {h.Id}",
							
				}).ToList(),
			};                 
                
            return View(createScreaningVM);
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateScreaning(CreateScreaningVM createScreaningVM)
        {

            
            if (!ModelState.IsValid)
                return View(createScreaningVM);

            Screaning screaning = new Screaning()
            {
                Day = createScreaningVM.Day,
                Schedule = createScreaningVM.Schedule,
                ScheduleId = createScreaningVM.ScheduleId,
                
                HallId = createScreaningVM.HallId,
            };
            await _adminRepository.AddScreaningAsync(screaning);
            await _adminRepository.GenerateTicketsAsync(screaning);
           
            return RedirectToAction("ListOfFilms");
        }
        [HttpGet]
        public async Task<IActionResult> ListOfFilms()
        {
            var films= await _filmsRepository.GetAllFilmsAsync();
            return View(films);
        }
		[HttpPost]
		public async Task<IActionResult> ListOfSchedulesForFilm(string filmJson)
		{
			var film=JsonSerializer.Deserialize<Film>(filmJson);
            return View(film);

        }

        [HttpPost]
        public async Task<IActionResult> ListOfScreaningForSchedule(string scheduleJson)
        {
            var schedule = JsonSerializer.Deserialize<Schedule>(scheduleJson);
            return View(schedule);

        }


    }
}
