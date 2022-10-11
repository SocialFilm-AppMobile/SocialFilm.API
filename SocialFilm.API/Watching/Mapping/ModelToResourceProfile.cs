using AutoMapper;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Resources;

namespace SocialFilm.API.Watching.Mapping;

public class ModelToResourceProfile:Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Category, CategoryResource>();
        CreateMap<Comment, CommentResource>();
        CreateMap<Film, FilmResource>();
        CreateMap<Serie, SerieResource>();
        CreateMap<Season, SeasonResource>();
        CreateMap<Episode, EpisodeResource>();
        CreateMap<BannerVideo, BannerVideoResource>();
    }
}