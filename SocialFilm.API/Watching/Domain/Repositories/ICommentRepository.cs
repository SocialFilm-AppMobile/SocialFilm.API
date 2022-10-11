using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<Comment>> ListAsync();
    Task AddAsync(Comment comment);
    Task<Comment> FindByIdAsync(int commentId);
    Task<IEnumerable<Comment>> FindByUserIdAsync(int userId);
    Task<IEnumerable<Comment>> FindByFilmIdAsync(int filmId);
    void Update(Comment comment);
    void Remove(Comment comment);

}