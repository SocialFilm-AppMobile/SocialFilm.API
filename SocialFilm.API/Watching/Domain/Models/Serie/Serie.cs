namespace SocialFilm.API.Watching.Domain.Models;

public class Serie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    //Relationship
    public int SeasonId { get; set; }
    public IList<Season> Seasons { get; set; } = new List<Season>();
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    //Likes Relationship
    //Qualification Relationship
}