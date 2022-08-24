using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Persistence.Contexts;

namespace SocialFilm.API.Watching.Persistence.Repositories;

public class EpisodeRepository:BaseRepository,IEpisodeRepository
{
    public EpisodeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Episode>> ListAsync()
    {
        return await _context.Episodes
            .Include(p => p.Video)
            .Include(p => p.Season)
            .ToListAsync();
    }

    public async Task AddAsync(Episode episode)
    {
        await _context.Episodes.AddAsync(episode);
    }

    public async Task<Episode> FindByIdAsync(int episodeId)
    {
        return await _context.Episodes
            .Include(p => p.Video)
            .Include(p => p.Season)
            .FirstOrDefaultAsync(p => p.Id == episodeId);
    }

    public async Task<Episode> FindByTitleAsync(string title)
    {
        return await _context.Episodes
            .Include(p => p.Video)
            .Include(p => p.Season)
            .FirstOrDefaultAsync(p => p.Title == title);
    }

    public async Task<IEnumerable<Episode>> FindBySeasonIdAsync(int seasonId)
    {
        return await _context.Episodes
            .Include(p => p.Season)
            .Include(p => p.Video)
            .Where(p => p.SeasonId == seasonId)
            .ToListAsync();
    }

    public void Update(Episode episode)
    {
        _context.Episodes.Update(episode);
    }

    public void Remove(Episode episode)
    {
        _context.Episodes.Remove(episode);
    }
}