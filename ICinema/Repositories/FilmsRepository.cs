using ICinema.Data;
using ICinema.Interfaces;
using ICinema.Models;
using Microsoft.EntityFrameworkCore;
using ICinema.ViewModels;
using System.Text.Json;
using CloudinaryDotNet.Actions;
namespace ICinema.Repositories
{
    public class FilmsRepository : IFilmsRepository
    {
        private readonly AppDBContext _appDBContext;
        public FilmsRepository(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public async Task AddAsync(Film film)
        {

            await _appDBContext.Films.AddAsync(film);
            await _appDBContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var film = await _appDBContext.Films.FindAsync(id);
            if (film != null)
            {
                _appDBContext.Films.Remove(film);
                await _appDBContext.SaveChangesAsync();
            }
        }

        public async Task<ICollection<FilmVM>> GetAllFilmsAsync()
        {
            var films = await _appDBContext.Films.ToListAsync();
            ICollection<FilmVM> filmsVM = new List<FilmVM>();
            foreach (var film in films)
            {
                var filmVM = new FilmVM()
                {
                    Id = film.Id,
                    Title = film.Title,
                    Image = film.Image
                };
                filmsVM.Add(filmVM);
            }
            return filmsVM;
        }

        public async Task<Film> GetByIdAsync(int id)
        {
            return await _appDBContext.Films.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task UpdateAsync(Film film)
        {
            var hallFromContext = await _appDBContext.Films.FirstOrDefaultAsync(h => h.Id == film.Id);
            if (hallFromContext != null)
            {
            }

        }

    }
}
