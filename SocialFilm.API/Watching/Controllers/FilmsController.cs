using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialFilm.API.Shared.Extensions;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Resources;
namespace SocialFilm.API.Watching.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class FilmsController : ControllerBase
{
    private readonly IFilmService _filmService;
    private readonly IMapper _mapper;

    public FilmsController(IFilmService filmService, IMapper mapper)
    {
        _filmService = filmService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<FilmResource>> GetAllAsync()
    {
        var films = await _filmService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Film>, IEnumerable<FilmResource>>(films);

        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<FilmResource> GetAsync(int id)
    {
        var film = await _filmService.GetAsync(id);
        var filmResource = _mapper.Map<Film, FilmResource>(film.Resource);

        return filmResource;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveFilmResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var film = _mapper.Map<SaveFilmResource, Film>(resource);

        var result = await _filmService.SaveAsync(film);

        if (!result.Success)
            return BadRequest(result.Message);

        var filmResource = _mapper.Map<Film, FilmResource>(result.Resource);

        return Ok(filmResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveFilmResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var film = _mapper.Map<SaveFilmResource, Film>(resource);
        var result = await _filmService.UpdateAsync(id, film);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var filmResource = _mapper.Map<Film, FilmResource>(result.Resource);

        return Ok(filmResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _filmService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var filmResource = _mapper.Map<Film, FilmResource>(result.Resource);

        return Ok(filmResource);
    }
}