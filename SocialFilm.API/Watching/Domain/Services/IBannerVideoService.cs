using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface IBannerVideoService
{
    Task<IEnumerable<BannerVideo>> ListAsync();
    Task<BannerVideoResponse> SaveAsync(BannerVideo bannerVideo);
    Task<BannerVideoResponse> UpdateAsync(int bannerVideoId, BannerVideo bannerVideo);
    Task<BannerVideoResponse> DeleteAsync(int bannerVideoId);
}