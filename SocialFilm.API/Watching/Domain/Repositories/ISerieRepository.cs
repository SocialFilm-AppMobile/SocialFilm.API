using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Repositories;

public interface ISerieRepository
{
    Task<IEnumerable<Serie>> ListAsync();
    Task AddAsync(Serie serie);
    Task<Serie> FindByIdAsync(int serieId);
    Task<Serie> FindByTitleAsync(string title);
    Task<IEnumerable<Serie>> FindByCategoryIdAsync(int categoryId);
    void Update(Serie serie);
    void Remove(Serie serie);
}