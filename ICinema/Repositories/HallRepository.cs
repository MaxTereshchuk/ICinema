using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<ICollection<Hall>> GetAllHallsAsync()
        {
            return await _appDBContext.Halls.ToListAsync();
        }

        public async Task<Hall> GetByIdAsync(int id)
        {
            return await _appDBContext.Halls.FirstOrDefaultAsync(h => h.Id == id);
        }

        public Task UpdateAsync(Hall hall)
        {
            throw new NotImplementedException();
        }
    }
}
