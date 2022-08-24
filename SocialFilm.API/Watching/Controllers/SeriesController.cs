using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialFilm.API.Shared.Extensions;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Resources;

namespace SocialFilm.API.Watching.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class SeriesController:ControllerBase
{
    private readonly ISerieService _serieService;
    private readonly IMapper _mapper;

    public SeriesController(ISerieService serieService, IMapper mapper)
    {
        _serieService = serieService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<SerieResource>> GetAllAsync()
    {
        var series = await _serieService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Serie>, IEnumerable<SerieResource>>(series);

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveSerieResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var serie = _mapper.Map<SaveSerieResource, Serie>(resource);

        var result = await _serieService.SaveAsync(serie);

        if (!result.Success)
            return BadRequest(result.Message);

        var serieResource = _mapper.Map<Serie, SerieResource>(result.Resource);

        return Ok(serieResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSerieResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var serie = _mapper.Map<SaveSerieResource, Serie>(resource);
        var result = await _serieService.UpdateAsync(id, serie);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var serieResource = _mapper.Map<Serie, SerieResource>(result.Resource);

        return Ok(serieResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _serieService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var serieResource = _mapper.Map<Serie, SerieResource>(result.Resource);

        return Ok(serieResource);
    }
}