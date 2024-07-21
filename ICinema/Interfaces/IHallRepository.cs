using ICinema.Models;
using ICinema.ViewModels;
namespace ICinema.Interfaces
{
    public interface IHallRepository
    {
        public Task<Hall> GetByIdAsync(int id);
        public Task AddAsync(Hall hall);
        public Task UpdateAsync(Hall hall);
        public Task DeleteAsync(int id);
        public Task<ICollection<HallVM>> GetAllHallsAsync();
    }
}
