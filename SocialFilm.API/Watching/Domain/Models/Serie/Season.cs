namespace SocialFilm.API.Watching.Domain.Models;

public class Season
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public int SerieId { get; set; }
    public Serie Serie { get; set; }

    //Relationship
    public IList<Episode> Episodes { get; set; } = new List<Episode>();
    

    //Likes Relationship
    //Qualification Relationship
}