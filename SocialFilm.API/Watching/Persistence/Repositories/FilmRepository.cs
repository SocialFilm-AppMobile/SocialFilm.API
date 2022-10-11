using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Shared.Persistence.Contexts;
using SocialFilm.API.Shared.Persistence.Repositories;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;

namespace SocialFilm.API.Watching.Persistence.Repositories;

public class FilmRepository:BaseRepository,IFilmRepository
{
    public FilmRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Film>> ListAsync()
    {
        return await _context.Films
            .Include(p => p.Category)
            .Include(p=>p.User)
            .Include(p=>p.BannerVideo)
            .ToListAsync();
    }

    public async Task AddAsync(Film film)
    {
        await _context.Films.AddAsync(film);
    }

    public async Task<Film> FindByIdAsync(int filmId)
    {
        return await _context.Films
            .Include(p => p.Category)
            .Include(p=>p.User)
            .Include(p=>p.BannerVideo)
            .FirstOrDefaultAsync(p => p.Id == filmId);
    }

    public async Task<Film> FindByTitleAsync(string title)
    {
        return await _context.Films
            .Include(p => p.Category)
            .Include(p=>p.User)
            .Include(p=>p.BannerVideo)
            .FirstOrDefaultAsync(p => p.Title == title);

    }

    public async Task<IEnumerable<Film>> FindByCategoryIdAsync(int categoryId)
    {
        return await _context.Films
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .Include(p=>p.User)
            .Include(p=>p.BannerVideo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Film>> FindByUserIdAsync(int userId)
    {
        return await _context.Films
            .Where(p => p.UserId == userId)
            .Include(p => p.Category)
            .Include(p=>p.User)
            .Include(p=>p.BannerVideo)
            .ToListAsync();
    }

    public void Update(Film film)
    {
        _context.Films.Update(film);
    }

    public void Remove(Film film)
    {
        _context.Films.Remove(film);
    }
}