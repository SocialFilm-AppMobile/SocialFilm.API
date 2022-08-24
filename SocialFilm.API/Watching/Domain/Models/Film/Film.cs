using SocialFilm.API.Shared.Domain.Model;

namespace SocialFilm.API.Watching.Domain.Models;

public class Film
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    
    //Relationship
    public int VideoId { get; set; }
    public Video Video { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public int BannerVideoId { get; set; }
    public BannerVideo BannerVideo { get; set; }
    
    //Likes Relationship
    //Qualification Relationship
}