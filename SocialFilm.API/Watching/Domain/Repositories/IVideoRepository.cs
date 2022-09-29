using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Repositories;

public interface IVideoRepository
{
    Task<IEnumerable<Video>> ListAsync();
    Task AddAsync(Video video);
    Task<Video> FindByIdAsync(int videoId);
    void Update(Video video);
    void Remove(Video video);
}