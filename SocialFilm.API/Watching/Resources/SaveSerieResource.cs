namespace SocialFilm.API.Watching.Resources;

public class SaveSerieResource
{
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    public int CategoryId { get; set; }
    public int? SeasonId { get; set; }
}