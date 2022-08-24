using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class FilmResponse : BaseResponse<Film>
{
    public FilmResponse(string message) : base(message)
    {
    }

    public FilmResponse(Film resource) : base(resource)
    {
    }
}