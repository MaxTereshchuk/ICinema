using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using Microsoft.EntityFrameworkCore;
using ICinema.ViewModels;
using System.Text.Json;
using CloudinaryDotNet.Actions;
namespace ICinema.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly AppDBContext _appDBContext;
        public HallRepository(AppDBContext appDBContext) {
            _appDBContext = appDBContext;
        }
        public async Task AddAsync(Hall hall)
        {
            
            await _appDBContext.Halls.AddAsync(hall);
            await _appDBContext.SaveChangesAsync();
  
        }

        public async Task DeleteAsync(int id)
        {            
            var hall = await _appDBContext.Halls.FindAsync(id);
            if (hall != null)
            {
                _appDBContext.Halls.Remove(hall);
                await _appDBContext.SaveChangesAsync();
            }            
        }

        public async Task<ICollection<HallVM>> GetAllHallsAsync()
        {
            var halls= await _appDBContext.Halls.ToListAsync();
            ICollection<HallVM> hallsVM=new List<HallVM>();
            foreach (var hall in halls)
            {
                var hallVM = new HallVM()
                {
                    Id = hall.Id,
                    Seats = JsonSerializer.Deserialize<List<List<Seat>>>(hall.SeatsJson)
                };
                hallsVM.Add(hallVM);
            }
            return hallsVM;
        }
        
        public async Task<Hall> GetByIdAsync(int id)
        {
            return await _appDBContext.Halls.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task UpdateAsync(Hall hall)
        {
            var hallFromContext= await  _appDBContext.Halls.FirstOrDefaultAsync(h => h.Id == hall.Id);
            if (hallFromContext != null)
            {
                hallFromContext.SeatsJson = hall.SeatsJson;
                await _appDBContext.SaveChangesAsync();
            }

        }
    }
}
