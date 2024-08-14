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
		public Task AddFilmAsync(Film film);
        public Task AddScheduleAsync(Schedule schedule);
        public Task AddScreaningAsync(Screaning screaning);

        public Task GenerateTicketsAsync(Screaning screaning);
    }
}
