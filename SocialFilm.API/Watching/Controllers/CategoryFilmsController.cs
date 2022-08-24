using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Resources;

namespace SocialFilm.API.Watching.Controllers;

[ApiController]
[Route("api/v1/categories/{categoryId}/films")]
public class CategoryFilmsController:ControllerBase
{
    private readonly IFilmService _filmService;
    private readonly IMapper _mapper;

    public CategoryFilmsController(IFilmService filmService, IMapper mapper)
    {
        _filmService = filmService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<FilmResource>> GetAllFilmsByCategoryIdAsync(int categoryId)
    {
        var films =  await _filmService.ListByCategoryIdAsync(categoryId);

        var resources = _mapper.Map<IEnumerable<Film>, IEnumerable<FilmResource>>(films);

        return resources;
    }
}