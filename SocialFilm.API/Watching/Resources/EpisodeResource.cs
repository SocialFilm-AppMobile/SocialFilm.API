namespace SocialFilm.API.Watching.Resources;

public class EpisodeResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    public VideoResource Video { get; set; }
}