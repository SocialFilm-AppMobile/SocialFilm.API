using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface ISeasonService
{
    Task<IEnumerable<Season>> ListAsync();
    Task<IEnumerable<Season>> ListBySerieIdAsync(int serieId);
    Task<SeasonResponse> SaveAsync(Season season);
    Task<SeasonResponse> SaveBySerieIdAsync(Season season, int serieId);
    Task<SeasonResponse> UpdateAsync(int seasonId, Season season);
    Task<SeasonResponse> UpdateBySerieIdAsync(int seasonId,Season season, int serieId);
    Task<SeasonResponse> DeleteAsync(int seasonId);
    Task<SeasonResponse> DeleteBySerieIdAsync(int seasonId,int serieId);
}