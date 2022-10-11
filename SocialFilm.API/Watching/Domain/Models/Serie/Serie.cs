namespace SocialFilm.API.Watching.Domain.Models;

public class Serie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    //Relationship
    public IList<Season> Seasons { get; set; }
    
    
    //Likes Relationship
    //Qualification Relationship
}