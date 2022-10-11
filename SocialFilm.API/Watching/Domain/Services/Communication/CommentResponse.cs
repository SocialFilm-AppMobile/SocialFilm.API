using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class CommentResponse:BaseResponse<Comment>
{
    public CommentResponse(string message) : base(message)
    {
    }

    public CommentResponse(Comment resource) : base(resource)
    {
    }
}