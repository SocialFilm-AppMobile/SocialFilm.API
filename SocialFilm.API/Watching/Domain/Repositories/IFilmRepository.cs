using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Repositories;

public interface IFilmRepository
{
    Task<IEnumerable<Film>> ListAsync();
    Task AddAsync(Film film);
    Task<Film> FindByIdAsync(int filmId);
    Task<Film> FindByTitleAsync(string title);
    Task<IEnumerable<Film>> FindByCategoryIdAsync(int categoryId);
    void Update(Film film);
    void Remove(Film film);
}