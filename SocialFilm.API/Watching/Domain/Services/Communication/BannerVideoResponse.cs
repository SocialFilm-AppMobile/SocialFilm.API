using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class BannerVideoResponse:BaseResponse<BannerVideo>
{
    public BannerVideoResponse(string message) : base(message)
    {
    }

    public BannerVideoResponse(BannerVideo resource) : base(resource)
    {
    }
}