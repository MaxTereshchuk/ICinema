using ICinema.Models;
using Microsoft.AspNetCore.Mvc;
using ICinema.ViewModels;
namespace ICinema.Interfaces
{
	public interface IAdminRepository
	{
		/// <summary>
		/// Admin method for creation new tickets (need to be changed to authomaticaly generation woth eddind new schedule of film)
		/// </summary>
		/// <param name="ticket"></param>
		/// <returns></returns>
		public Task<bool> CreateTicket(Ticket ticket);
		public Task<bool> AddEmailSettings(EmailSettings emailSettings);
		public Task CreateFilmAsync(CreateFilmVM createFilmVM);
        public Task CreateScheduleAsync(CreateScheduleVM createSheduleVM);
        public Task CreateScreaningAsync(CreateScreaningVM createScreaningVM);

        public Task GenerateTicketsAsync(Screaning screaning);
    }
}
