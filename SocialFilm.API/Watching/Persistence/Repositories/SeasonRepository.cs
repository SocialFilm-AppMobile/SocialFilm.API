using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Persistence.Contexts;

namespace SocialFilm.API.Watching.Persistence.Repositories;

public class SeasonRepository:BaseRepository,ISeasonRepository
{
    public SeasonRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Season>> ListAsync()
    {
        return await _context.Seasons
            .Include(p=>p.Episodes)
            .ToListAsync();
    }

    public async Task AddAsync(Season season)
    {
        await _context.Seasons.AddAsync(season);
    }

    public async Task<Season> FindByIdAsync(int seasonId)
    {
        return await _context.Seasons
            .Include(p => p.Episodes)
            .FirstOrDefaultAsync(p => p.Id == seasonId);
    }

    public async Task<IEnumerable<Season>> FindBySerieIdAsync(int serieId)
    {
        return await _context.Seasons
            .Include(p => p.Serie)
            .Include(p => p.Episodes)
            .Where(p => p.SerieId == serieId)
            .ToListAsync();
    }

    public async Task<Season> FindByTitleAsync(string title)
    {
        return await _context.Seasons
            .Include(p => p.Episodes)
            .FirstOrDefaultAsync(p => p.Title == title);
    }

    public void Update(Season season)
    {
        _context.Seasons.Update(season);
    }

    public void Remove(Season season)
    {
        _context.Seasons.Remove(season);
    }
}