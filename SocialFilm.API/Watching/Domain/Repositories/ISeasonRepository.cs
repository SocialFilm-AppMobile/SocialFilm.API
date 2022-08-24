using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Repositories;

public interface ISeasonRepository
{
    Task<IEnumerable<Season>> ListAsync();
    Task AddAsync(Season season);
    Task<Season> FindByIdAsync(int seasonId);
    Task<IEnumerable<Season>> FindBySerieIdAsync(int serieId);
    Task<Season> FindByTitleAsync(string title);
    void Update(Season season);
    void Remove(Season season);
}