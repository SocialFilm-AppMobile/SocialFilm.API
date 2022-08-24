using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface ISerieService
{
    Task<IEnumerable<Serie>> ListAsync();
    Task<IEnumerable<Serie>> ListByCategoryIdAsync(int categoryId);
    Task<SerieResponse> SaveAsync(Serie serie);
    Task<SerieResponse> UpdateAsync(int serieId, Serie serie);
    Task<SerieResponse> DeleteAsync(int serieId);
}