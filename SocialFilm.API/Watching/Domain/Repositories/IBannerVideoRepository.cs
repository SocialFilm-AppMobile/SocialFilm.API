using SocialFilm.API.Shared.Domain.Model;

namespace SocialFilm.API.Watching.Domain.Repositories;

public interface IBannerVideoRepository
{
    Task<IEnumerable<BannerVideo>> ListAsync();
    Task AddAsync(BannerVideo bannerVideo);
    Task<BannerVideo> FindByIdAsync(int bannerVideoId);
    void Update(BannerVideo bannerVideo);
    void Remove(BannerVideo bannerVideo);
}