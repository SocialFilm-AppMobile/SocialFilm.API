using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Shared.Domain.Model;

public class BannerVideo
{
    public int Id { get; set; }
    public string Billboard { get; set; }
    public string Banner { get; set; }
    
    public int FilmId { get; set; }
    public Film Film { get; set; }
}