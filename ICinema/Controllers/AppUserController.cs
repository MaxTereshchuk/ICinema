using Microsoft.AspNetCore.Mvc;

namespace ICinema.Controllers
{
    public class AppUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
