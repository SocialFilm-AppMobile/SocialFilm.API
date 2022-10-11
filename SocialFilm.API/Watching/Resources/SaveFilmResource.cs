using System.Diagnostics.CodeAnalysis;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Resources;

public class SaveFilmResource
{
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public string VideoUrl { get; set; }
    
    public int CategoryId { get; set; }
    public int UserId { get; set; }
    
    
    public SaveBannerVideoResource BannerVideo { get; set; }
}