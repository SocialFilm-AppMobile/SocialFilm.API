using SocialFilm.API.Shared.Domain.Services.Comunication;
using SocialFilm.API.Watching.Domain.Models;

namespace SocialFilm.API.Watching.Domain.Services.Communication;

public class CategoryResponse: BaseResponse<Category>
{
    public CategoryResponse(string message) : base(message)
    {
    }

    public CategoryResponse(Category resource) : base(resource)
    {
    }
}