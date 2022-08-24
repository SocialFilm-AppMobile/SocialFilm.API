using AutoMapper;
using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Resources;

namespace SocialFilm.API.Watching.Mapping;

public class ModelToResourceProfile:Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Category, CategoryResource>();
        CreateMap<Video, VideoResource>();
        CreateMap<Film, FilmResource>();
        CreateMap<Serie, SerieResource>();
        CreateMap<Season, SeasonResource>();
        CreateMap<Episode, EpisodeResource>();
        CreateMap<BannerVideo, BannerVideoResource>();
    }
}