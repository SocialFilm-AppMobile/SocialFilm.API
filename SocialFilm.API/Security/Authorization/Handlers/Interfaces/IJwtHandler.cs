using SocialFilm.API.Security.Domain.Models;

namespace SocialFilm.API.Security.Authorization.Handlers.Interfaces;

public interface IJwtHandler
{
    string GenerateToken(User user);
    int? ValidateToken(string token);
}