using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class SerieResponse: BaseResponse<Serie>
{
    public SerieResponse(string message) : base(message)
    {
    }

    public SerieResponse(Serie resource) : base(resource)
    {
    }
}