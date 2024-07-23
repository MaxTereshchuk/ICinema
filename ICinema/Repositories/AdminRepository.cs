using System.Runtime.InteropServices;
using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ICinema.Repositories
{
	public class AdminRepository : IAdminRepository
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly AppDBContext _appDBContext;
		private readonly IEmailSender _emailSender;
		public AdminRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDBContext appDBContext, IEmailSender emailSender)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_appDBContext = appDBContext;
			_emailSender = emailSender;
		}

        public  async Task<bool> AddEmailSettings(EmailSettings emailSettings)
        {
			var _isExisting = await _appDBContext.EmailSettings.FirstOrDefaultAsync(e => e.Id == 1);
			if (_isExisting == null) 
			{
				try
				{
					await _appDBContext.EmailSettings.AddAsync(emailSettings);
                    await _appDBContext.SaveChangesAsync();
                    return true;
				}
				catch
				{
					
					return false;
				}
			}
			return false;
        }

        public Task CreateFilmAsync(CreateFilmVM createFilmVM)
        {
            throw new NotImplementedException();
        }

        public Task CreateScheduleAsync(CreateScheduleVM createSheduleVM)
        {
            throw new NotImplementedException();
        }

        public Task CreateScreaningAsync(CreateScreaningVM createScreaningVM)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateTicket(Ticket ticket)
		{
			var ticketCheack =await _appDBContext.Tickets.FirstOrDefaultAsync(t => t.ScreaningId == ticket.ScreaningId && t.SeatNumber==ticket.SeatNumber && t.RowNumber==ticket.RowNumber);
			if (ticketCheack!=null)
			{
				return false; 
			}
			try{
				await _appDBContext.Tickets.AddAsync(ticket);
				await _appDBContext.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}			
		}

        public Task GenerateTicketsAsync(Screaning screaning)
        {
            throw new NotImplementedException();
        }
    }
}
