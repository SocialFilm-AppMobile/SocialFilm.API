using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class SeasonResponse: BaseResponse<Season>
{
    public SeasonResponse(string message) : base(message)
    {
    }

    public SeasonResponse(Season resource) : base(resource)
    {
    }
}