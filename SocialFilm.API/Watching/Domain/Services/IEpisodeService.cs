using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface IEpisodeService
{
    Task<IEnumerable<Episode>> ListAsync();
    Task<IEnumerable<Episode>> ListBySeasonIdAsync(int seasonId);
    Task<EpisodeResponse> SaveAsync(Episode episode);
    
    Task<EpisodeResponse> SaveBySeasonIdAsync(int seasonId, Episode episode);
    Task<EpisodeResponse> UpdateAsync(int episodeId, Episode episode);
    Task<EpisodeResponse> UpdateBySeasonIdAsync(int seasonId, int episodeId, Episode episode);
    Task<EpisodeResponse> DeleteAsync(int episodeId);
}