using SocialFilm.API.Security.Domain.Models;
using SocialFilm.API.Security.Resources;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Resources;

public class CommentResource
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public UserResource User { get; set; }
    
    public FilmResource Film { get; set; }
   
}