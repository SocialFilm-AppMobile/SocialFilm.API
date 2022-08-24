namespace SocialFilm.API.Watching.Resources;

public class SerieResource
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    public CategoryResource Category { get; set; }
}