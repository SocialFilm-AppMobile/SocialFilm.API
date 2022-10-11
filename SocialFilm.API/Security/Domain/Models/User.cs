using System.Text.Json.Serialization;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Security.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string PasswordHash { get; set; }
    
    public IList<Comment> Comments { get; set; }
    public IList<Film> Films { get; set; }

}