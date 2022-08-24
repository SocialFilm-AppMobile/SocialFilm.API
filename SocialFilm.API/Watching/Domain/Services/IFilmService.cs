using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface IFilmService
{
    Task<IEnumerable<Film>> ListAsync();
    Task<IEnumerable<Film>> ListByCategoryIdAsync(int categoryId);
    Task<FilmResponse> GetAsync(int filmId);
    Task<FilmResponse> SaveAsync(Film film);
    Task<FilmResponse> UpdateAsync(int filmId, Film film);
    Task<FilmResponse> DeleteAsync(int filmId);
}