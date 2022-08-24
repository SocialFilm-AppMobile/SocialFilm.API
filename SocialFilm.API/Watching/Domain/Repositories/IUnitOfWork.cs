namespace SocialFilm.API.Watching.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}