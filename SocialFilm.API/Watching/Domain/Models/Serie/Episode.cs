namespace SocialFilm.API.Watching.Domain.Models;

public class Episode
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    public string VideoUrl { get; set; }

    //Relationship
    
    public int SeasonId { get; set; }
    public Season Season { get; set; }
    
    
    
}