using ICinema.Data;
using ICinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace ICinema.Controllers
{
    public class FilmController : Controller
    {
        private readonly AppDBContext _context;
        public FilmController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Film> films = _context.Films.ToList();
            return View(films);
        }
    }
}
