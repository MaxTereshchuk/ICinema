using Microsoft.AspNetCore.Mvc;

namespace ICinema.Controllers
{
    public class HallController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
