using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Resources;

namespace SocialFilm.API.Security.Controllers;

[ApiController]
[Route("api/v1/users/{userId}/films")]
public class UserFilmsController:ControllerBase
{
    private readonly IFilmService _filmService;
    private readonly IMapper _mapper;

    public UserFilmsController(IFilmService filmService, IMapper mapper)
    {
        _filmService = filmService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<FilmResource>> GetAllFilmsByUserIdAsync(int userId)
    {
        var films = await _filmService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Film>, IEnumerable<FilmResource>>(films);
        return resources;
    }




}