using AutoMapper;
using SocialFilm.API.Security.Domain.Models;
using SocialFilm.API.Security.Domain.Services.Communication;
using SocialFilm.API.Security.Resources;

namespace SocialFilm.API.Security.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<User, AuthenticateResponse>();
        CreateMap<User, UserResource>();
    }
}