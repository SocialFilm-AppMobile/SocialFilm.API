using AutoMapper;
using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Resources;

namespace SocialFilm.API.Watching.Mapping;

public class ResourceToModelProfile:Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveCategoryResource, Category>();
        CreateMap<SaveVideoResource, Video>();
        CreateMap<SaveFilmResource, Film>();
        CreateMap<SaveSerieResource,Serie>();
        CreateMap<SaveSeasonResource, Season>();
        CreateMap<SaveEpisodeResource, Episode>();
        CreateMap<SaveBannerVideoResource, BannerVideo>();
    }
}