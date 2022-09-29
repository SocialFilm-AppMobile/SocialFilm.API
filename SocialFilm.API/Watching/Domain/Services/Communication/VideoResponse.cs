using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class VideoResponse:BaseResponse<Video>
{
    public VideoResponse(string message) : base(message)
    {
    }

    public VideoResponse(Video resource) : base(resource)
    {
    }
}