using System.Runtime.InteropServices;
using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using ICinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Collections.Generic;
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

        public async Task AddFilmAsync(Film film)
        {
			await _appDBContext.Films.AddAsync(film);
			await _appDBContext.SaveChangesAsync();
			
        }

        public async Task AddScheduleAsync(Schedule schedule)
        {
            var Film = schedule.Film;

			schedule.Film = null;
            await _appDBContext.Schedules.AddAsync(schedule);
            await _appDBContext.SaveChangesAsync();
            schedule.Film = Film;
        }

        public async Task AddScreaningAsync(Screaning screaning)
        {
            var Schedule=screaning.Schedule;

			screaning.Schedule = null;
            await _appDBContext.Screanings.AddAsync(screaning);
            await _appDBContext.SaveChangesAsync();
            screaning.Schedule = Schedule;
        }

        public async Task<bool> CreateTicket(Ticket ticket)
        {
            
            var ticketCheack = await _appDBContext.Tickets.FirstOrDefaultAsync(t => t.ScreaningId == ticket.ScreaningId && t.SeatNumber == ticket.SeatNumber && t.RowNumber == ticket.RowNumber);
            if (ticketCheack != null)
            {
                return false;
            }
            try
            {
                ticket.Screaning = null;
                await _appDBContext.Tickets.AddAsync(ticket);
                await _appDBContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async  Task GenerateTicketsAsync(Screaning screaning)
        {
            var Hall = await _appDBContext.Halls.FirstOrDefaultAsync(h => h.Id==screaning.HallId);
            screaning.Hall = Hall;
			List<List<Seat>> Seats = JsonSerializer.Deserialize<List<List<Seat>>>(screaning.Hall.SeatsJson);
           

            for(int row=0; row<Seats.Count; row++)
            {
                for (int seat = 0; seat < Seats[row].Count; seat++)
                {
                    Ticket ticket = new Ticket()
                    {
                        ScreaningId = screaning.Id,
                        Screaning = screaning,
                        SeatNumber = seat+1,
                        RowNumber = row+1,
                        Price = 180,
                        _isOccupied = false,
                    };
                    await _appDBContext.Tickets.AddAsync(ticket);
					await _appDBContext.SaveChangesAsync();
				}
            }
        }

    }
}
