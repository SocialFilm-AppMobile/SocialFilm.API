using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface ICommentService
{
    Task<IEnumerable<Comment>> ListAsync();
    Task<IEnumerable<Comment>> ListByUserIdAsync(int userId);
    Task<IEnumerable<Comment>> ListByFilmIdAsync(int filmId);
    Task<CommentResponse> SaveAsync(Comment comment,int filmId);
    Task<CommentResponse> UpdateAsync(int commentId, Comment comment);
    Task<CommentResponse> DeleteAsync(int commentId);


}