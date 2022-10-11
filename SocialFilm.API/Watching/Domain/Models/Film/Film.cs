using SocialFilm.API.Security.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Models;

public class Film
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Synopsis { get; set; }
    public string VideoURL { get; set; }
    public IList<Comment> Comments { get; set; }
    //Relationship
    public int UserId { get; set; }
    public User User { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BannerVideoId { get; set; }
    public BannerVideo BannerVideo { get; set; }
    
    //Likes Relationship
    //Qualification Relationship
}