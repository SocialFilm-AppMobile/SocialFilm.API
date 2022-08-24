using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Persistence.Contexts;

namespace SocialFilm.API.Watching.Persistence.Repositories;

public class VideoRepository:BaseRepository,IVideoRepository
{
    public VideoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Video>> ListAsync()
    {
        return await _context.Videos.ToListAsync();
    }

    public async Task AddAsync(Video video)
    {
        await _context.Videos.AddAsync(video);
    }

    public async Task<Video> FindByIdAsync(int videoId)
    {
        return await _context.Videos.FindAsync(videoId);
    }

    public void Update(Video video)
    {
        _context.Videos.Update(video);
    }

    public void Remove(Video video)
    {
        _context.Videos.Remove(video);
    }
}