using SocialFilm.API.Shared.Domain.Repositories;
using SocialFilm.API.Shared.Persistence.Contexts;
using SocialFilm.API.Watching.Domain.Repositories;

namespace SocialFilm.API.Shared.Persistence.Repositories;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}