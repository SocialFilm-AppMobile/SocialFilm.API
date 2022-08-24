using System.Diagnostics.CodeAnalysis;
using SocialFilm.API.Shared.Domain.Model;

namespace SocialFilm.API.Watching.Resources;

public class SaveFilmResource
{
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    public int? CategoryId { get; set; }
    
    public Video? Video { get; set; }
    public SaveBannerVideoResource? BannerVideo { get; set; }
}