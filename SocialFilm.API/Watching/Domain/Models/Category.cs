namespace SocialFilm.API.Watching.Domain.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    // Relationships
    public IList<Film> Films { get; set; } = new List<Film>();
    public IList<Serie> Series { get; set; } = new List<Serie>();
}