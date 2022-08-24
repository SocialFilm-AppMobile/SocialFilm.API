using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Shared.Domain.Services.Comunication;

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