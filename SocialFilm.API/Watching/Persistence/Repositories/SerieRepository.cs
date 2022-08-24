using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Persistence.Contexts;

namespace SocialFilm.API.Watching.Persistence.Repositories;

public class SerieRepository:BaseRepository,ISerieRepository
{
    public SerieRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Serie>> ListAsync()
    {
        return await _context.Series
            .Include(p=>p.Category)
            .Include(p=>p.Seasons)
            .ToListAsync();
    }

    public async Task AddAsync(Serie serie)
    {
        await _context.Series.AddAsync(serie);
    }

    public async Task<Serie> FindByIdAsync(int serieId)
    {
        return await _context.Series
            .Include(p => p.Category)
            .Include(p => p.Seasons)
            .FirstOrDefaultAsync(p => p.Id == serieId);
    }

    public async Task<Serie> FindByTitleAsync(string title)
    {
        return await _context.Series
            .Include(p => p.Category)
            .Include(p => p.Seasons)
            .FirstOrDefaultAsync(p => p.Title == title);
    }

    public async Task<IEnumerable<Serie>> FindByCategoryIdAsync(int categoryId)
    {
        return await _context.Series
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .Include(p => p.Seasons)
            .ToListAsync();
    }

    public void Update(Serie serie)
    {
        _context.Series.Update(serie);
    }

    public void Remove(Serie serie)
    {
        _context.Series.Remove(serie);
    }
}