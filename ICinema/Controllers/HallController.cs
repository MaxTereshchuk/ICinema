using System.Net.NetworkInformation;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
namespace ICinema.Controllers
{
    public class HallController : Controller
    {
        /// <summary>
        /// This action designed for definite film's hall
        /// </summary>
        /// <param name="hall"></param>
        /// <returns></returns>
        public IActionResult DefiniteHall(Hall hall)
        {
            return View();
        }
        [HttpPost]
        public  IActionResult AddRow(HallVM hallVM, string SeatsJson)
        {
            hallVM.Seats = JsonSerializer.Deserialize<List<List<bool>>>(SeatsJson);
            hallVM.Seats.Add(new List<bool>());
            hallVM.Rows++;
           
            return RedirectToAction("CreateHall", "Admin", hallVM);
        }
		[HttpPost]
		public IActionResult AddSeat(HallVM hallVM, int definiteRow)
		{
			var rowlength=hallVM.Seats[definiteRow].Count;
			hallVM.Seats[definiteRow].Add(false);
			return RedirectToAction("CreateHall", "Admin", hallVM);
		}
		[HttpPost]
		public IActionResult DeleteRow(HallVM hallVM)
		{
			hallVM.Rows--;
            hallVM.Seats.RemoveAt(hallVM.Rows);
            return RedirectToAction("CreateHall", "Admin", hallVM);
		}
		[HttpPost]
		public IActionResult DeleteSeat(HallVM hallVM, int definiteRow)

        {
            var rowlength = hallVM.Seats[definiteRow].Count;
            hallVM.Seats[definiteRow].RemoveAt(rowlength);
            return RedirectToAction("CreateHall", "Admin", hallVM);
        }
	}
}
