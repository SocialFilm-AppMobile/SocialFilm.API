using SocialFilm.API.Shared.Domain.Model;

namespace SocialFilm.API.Watching.Domain.Models;

public class Episode
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    //Relationship
    
    public int SeasonId { get; set; }
    public Season Season { get; set; }
    public int VideoId { get; set; }
    public Video Video { get; set; }
    
    //Likes Relationship
    //Qualification Relationship
}