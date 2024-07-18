using ICinema.Data;
using ICinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace ICinema.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly AppDBContext _context;
        public ScheduleController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Schedule> schedules = _context.Schedules.ToList();
            return View(schedules);
        }
    }

}
