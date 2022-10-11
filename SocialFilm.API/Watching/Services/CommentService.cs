using SocialFilm.API.Security.Domain.Repositories;
using SocialFilm.API.Shared.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Services;

public class CommentService:ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilmRepository _filmRepository;
    private readonly IUserRepository _userRepository;

    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IFilmRepository filmRepository, IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _filmRepository = filmRepository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Comment>> ListAsync()
    {
        return await _commentRepository.ListAsync();
    }

    public async Task<IEnumerable<Comment>> ListByUserIdAsync(int userId)
    {
        return await _commentRepository.FindByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Comment>> ListByFilmIdAsync(int filmId)
    {
        return await _commentRepository.FindByFilmIdAsync(filmId);
    }

    public async Task<CommentResponse> SaveAsync(Comment comment,int filmId)
    {
        var existingUser = await _userRepository.FindByIdAsync(comment.UserId);
        var existingFilm = await _filmRepository.FindByIdAsync(filmId);

        if (existingUser == null)
            return new CommentResponse("Invalid User");
        
        
        if (existingFilm == null)
            return new CommentResponse("Invalid Film :'v");
        

        try
        {
            comment.FilmId = filmId;
            await _commentRepository.AddAsync(comment);
            await _unitOfWork.CompleteAsync();
            return new CommentResponse(comment);
        }
        catch (Exception e)
        {
            return new CommentResponse($"An error occurred while saving the comment: {e.Message}");
        }
    }

    public async Task<CommentResponse> UpdateAsync(int commentId, Comment comment)
    {
        var existingComment = await _commentRepository.FindByIdAsync(commentId);
        var existingUser = await _userRepository.FindByIdAsync(comment.UserId);
        var existingFilm = await _filmRepository.FindByIdAsync(comment.FilmId);

        if (existingUser == null)
            return new CommentResponse("Invalid User");
        if (existingFilm == null)
            return new CommentResponse("Invalid Film");
        if (existingComment == null)
            return new CommentResponse("Comment not found");

        existingComment.Content = comment.Content;

        try
        {
            _commentRepository.Update(existingComment);
            await _unitOfWork.CompleteAsync();
            return new CommentResponse(existingComment);
        }
        catch(Exception e)
        {
            return new CommentResponse($"An error occurred while updating the comment: {e.Message}");
        }

    }

    public async Task<CommentResponse> DeleteAsync(int commentId)
    {
        var existingComment = await _commentRepository.FindByIdAsync(commentId);
        if (existingComment == null)
            return new CommentResponse("Comment not found");
        try
        {
            _commentRepository.Remove(existingComment);
            await _unitOfWork.CompleteAsync();
            return new CommentResponse(existingComment);
        }
        catch(Exception e)
        {
            return new CommentResponse($"An error occurred while deleting the comment: {e.Message}");
        }
    }
}