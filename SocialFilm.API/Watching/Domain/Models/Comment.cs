using SocialFilm.API.Security.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public User User { get; set; }
    public int UserId { get; set; }

    public Film Film { get; set; }
    public int FilmId { get; set; }
    

    
}