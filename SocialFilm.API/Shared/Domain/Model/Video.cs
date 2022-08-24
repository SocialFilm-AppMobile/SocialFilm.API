using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Shared.Domain.Model;

public class Video
{
    public int Id { get; set; }
    public string VideoUrl { get; set; }
    
    // Relationships
    //public IList<Film> Films { get; set; } = new List<Film>();
    //public IList<Episode> Episodes { get; set; } = new List<Episode>();
}