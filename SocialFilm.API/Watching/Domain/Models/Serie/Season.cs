namespace SocialFilm.API.Watching.Domain.Models;

public class Season
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    //Relationship
    public int EpisodeId { get; set; }
    public IList<Episode> Episodes { get; set; } = new List<Episode>();
    
    public int SerieId { get; set; }
    public Serie Serie { get; set; }
    
    //Likes Relationship
    //Qualification Relationship
}