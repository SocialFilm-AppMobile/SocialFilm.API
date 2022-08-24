using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Domain.Services;

public interface ICategoryService
{
    Task<IEnumerable<Category>> ListAsync();
    Task<CategoryResponse> SaveAsync(Category category);
    Task<CategoryResponse> UpdateAsync(int categoryId, Category category);
    Task<CategoryResponse> DeleteAsync(int categoryId);
}