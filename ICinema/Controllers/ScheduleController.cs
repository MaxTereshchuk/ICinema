using ICinema.Data;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ICinema.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly AppDBContext _context;
        public ScheduleController(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var schedules = await _context.Schedules
                .Include(s => s.Film)
                .ToListAsync();

            var scheduleViewModels = schedules.Select(s => new ScheduleViewModel
            {
                Film = s.Film,
                Schedule = s
            }).ToList();

            return View(scheduleViewModels);
        }
    }

}
