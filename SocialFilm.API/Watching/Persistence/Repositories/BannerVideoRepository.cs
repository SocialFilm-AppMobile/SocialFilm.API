using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Persistence.Contexts;

namespace SocialFilm.API.Watching.Persistence.Repositories;

public class BannerVideoRepository:BaseRepository,IBannerVideoRepository
{
    public BannerVideoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<BannerVideo>> ListAsync()
    {
        return await _context.BannerVideos.ToListAsync();
    }

    public async Task AddAsync(BannerVideo bannerVideo)
    {
        await _context.BannerVideos.AddAsync(bannerVideo);
    }

    public async Task<BannerVideo> FindByIdAsync(int bannerVideoId)
    {
        return await _context.BannerVideos.FindAsync(bannerVideoId);
    }

    public void Update(BannerVideo bannerVideo)
    {
        _context.BannerVideos.Update(bannerVideo);
    }

    public void Remove(BannerVideo bannerVideo)
    {
        _context.BannerVideos.Remove(bannerVideo);
    }
}