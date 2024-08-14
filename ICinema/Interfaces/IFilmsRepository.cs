using ICinema.Models;
using ICinema.ViewModels;
namespace ICinema.Interfaces
{
    public interface IFilmsRepository
    {
        public Task<Film> GetByIdAsync(int id);
        public Task AddAsync(Film film);
        public Task UpdateAsync(Film film);
        public Task DeleteAsync(int id);
        public Task<ICollection<FilmVM>> GetAllFilmsAsync();
    }
}
