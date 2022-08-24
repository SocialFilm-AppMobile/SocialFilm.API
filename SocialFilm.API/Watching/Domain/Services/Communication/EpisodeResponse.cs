using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class EpisodeResponse: BaseResponse<Episode>
{
    public EpisodeResponse(string message) : base(message)
    {
    }

    public EpisodeResponse(Episode resource) : base(resource)
    {
    }
}