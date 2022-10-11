using Microsoft.EntityFrameworkCore;
using SocialFilm.API.Shared.Persistence.Contexts;
using SocialFilm.API.Shared.Persistence.Repositories;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;

namespace SocialFilm.API.Watching.Persistence.Repositories;

public class CommentRepository:BaseRepository,ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _context.Comments
            .Include(p => p.Film)
            .Include(p => p.User)
            .Include(p=>p.Film.BannerVideo)
            .Include(p=>p.Film.Category)
            .ToListAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
    }

    public async Task<Comment> FindByIdAsync(int commentId)
    {
        return await _context.Comments
            .Include(p => p.Film)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == commentId);
    }

    public async Task<IEnumerable<Comment>> FindByUserIdAsync(int userId)
    {
        return await _context.Comments
            .Where(p => p.UserId == userId)
            .Include(p => p.Film)
            .Include(p => p.User)
            .ToListAsync();
    }

    public async Task<IEnumerable<Comment>> FindByFilmIdAsync(int filmId)
    {
        return await _context.Comments
            .Where(p => p.FilmId == filmId)
            .Include(p => p.Film)
            .Include(p => p.User)
            .ToListAsync();
    }

    public void Update(Comment comment)
    {
        _context.Comments.Update(comment);
    }

    public void Remove(Comment comment)
    {
        _context.Comments.Remove(comment);
    }
}