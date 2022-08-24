using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface IVideoService
{
    Task<IEnumerable<Video>> ListAsync();
    Task<VideoResponse> SaveAsync(Video video);
    Task<VideoResponse> UpdateAsync(int videoId, Video video);
    Task<VideoResponse> DeleteAsync(int videoId);
}