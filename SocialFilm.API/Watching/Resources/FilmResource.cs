namespace SocialFilm.API.Watching.Resources;

public class FilmResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    public CategoryResource Category { get; set; }
    public VideoResource Video { get; set; }
    public BannerVideoResource BannerVideo { get; set; }
}