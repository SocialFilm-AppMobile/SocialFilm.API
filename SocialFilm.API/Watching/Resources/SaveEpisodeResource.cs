using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Resources;

public class SaveEpisodeResource
{
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public string VideoUrl { get; set; }
    
    public int SeasonId { get; set; }
    
}