using System.Net.NetworkInformation;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using ICinema.Data;
using ICinema.Interfaces;

namespace ICinema.Controllers
{
    public class HallController : Controller
    {
        private readonly  IHallRepository _hallRepository;

        public HallController(IHallRepository hallRepository)
        {
            _hallRepository = hallRepository;
        }



        /// <summary>
        /// This action designed for definite film's hall
        /// </summary>
        /// <param name="hall"></param>
        /// TempData["hall"]= JsonSerialize.Serialize<Hall>(hall)
        /// <returns></returns>  return RedirectToAction("DefiniteHall", "Hall")
        public IActionResult DefiniteHall()
        {
            
            return View();
        }
        public async Task<Hall> GetHallById(int id)
        {
            var hall = await _hallRepository.GetByIdAsync(id);
            TempData["Hall"] = JsonSerializer.Serialize(hall);
            
            return hall;
        }
        [HttpPost]
        public  IActionResult AddRow(string HallVMJson)
        {
            var hallVM=new HallVM();

            hallVM= JsonSerializer.Deserialize<HallVM>(HallVMJson);
            hallVM.Seats.Add(new List<Seat>());
            
            TempData["HallVM"] = JsonSerializer.Serialize(hallVM);
            return RedirectToAction("CreateHall", "Admin");
        }
		[HttpPost]
		public IActionResult AddSeat(string HallVMJson, int definiteRow)
		{
            var hallVM = new HallVM();

            hallVM = JsonSerializer.Deserialize<HallVM>(HallVMJson);
            
			hallVM.Seats[definiteRow].Add(new Seat());
            TempData["HallVM"]=JsonSerializer.Serialize(hallVM);
			return RedirectToAction("CreateHall", "Admin");
		}
        [HttpPost]
        public IActionResult AddColumn(string HallVMJson, int definiteRow)
        {
            var hallVM = new HallVM();

            hallVM = JsonSerializer.Deserialize<HallVM>(HallVMJson);

            hallVM.Seats[definiteRow].Add(new Seat() { _IsColumn=true });
            TempData["HallVM"] = JsonSerializer.Serialize(hallVM);
            return RedirectToAction("CreateHall", "Admin");
        }
        [HttpPost]
		public IActionResult DeleteRow(string HallVMJson)
		{
            var hallVM = new HallVM();

            hallVM = JsonSerializer.Deserialize<HallVM>(HallVMJson);
            if (hallVM.Rows-1 != -1)
                hallVM.Seats.RemoveAt(hallVM.Rows - 1);
            

            
            TempData["HallVM"] = JsonSerializer.Serialize(hallVM);
            return RedirectToAction("CreateHall", "Admin");
		}
		[HttpPost]
		public IActionResult DeleteSeat(string HallVMJson, int definiteRow)
        {
            var hallVM = new HallVM();

            hallVM = JsonSerializer.Deserialize<HallVM>(HallVMJson);
            var rowlength = hallVM.Seats[definiteRow].Count-1;
            if (rowlength > -1) 
                hallVM.Seats[definiteRow].RemoveAt(rowlength);
            TempData["HallVM"] = JsonSerializer.Serialize(hallVM);
            return RedirectToAction("CreateHall", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> Save(string HallVMJson)
        {
            var hallVM = new HallVM();
            hallVM =  JsonSerializer.Deserialize<HallVM>(HallVMJson);
            if (!ModelState.IsValid)
            {
                return View(hallVM);
            }
            Hall hall = new Hall()
            {
                Id = hallVM.Id,
                SeatsJson = JsonSerializer.Serialize<List<List<Seat>>>(hallVM.Seats)
            };
            var hallcheack = await _hallRepository.GetByIdAsync(hall.Id);
            if (hallcheack != null)
            {
                await _hallRepository.UpdateAsync(hall);
                return RedirectToAction("Index", "Home");
            }
            _hallRepository.AddAsync(hall);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllHalls()
        {
            var hallsVM=await _hallRepository.GetAllHallsAsync();
            string action = TempData["ActionToRedirect"].ToString();
			string controller = TempData["ControllerToRedirect"].ToString();
			TempData["hallsVMJson"]= JsonSerializer.Serialize(hallsVM);
            return RedirectToAction(action, controller);
        }
	}
}
