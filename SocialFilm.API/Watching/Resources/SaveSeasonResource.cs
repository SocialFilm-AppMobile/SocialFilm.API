namespace SocialFilm.API.Watching.Resources;

public class SaveSeasonResource
{
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    //public int SerieId { get; set; }
    public int? EpisodeId { get; set; }
}