using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialFilm.API.Shared.Extensions;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Resources;
using SocialFilm.API.Watching.Services;

namespace SocialFilm.API.Watching.Controllers;

[ApiController]
[Route("api/v1/series/{serieId}/seasons")]
public class SerieSeasonsController:ControllerBase
{
    private readonly ISeasonService _seasonService;
    private readonly IMapper _mapper;

    public SerieSeasonsController(ISeasonService seasonService, IMapper mapper)
    {
        _seasonService = seasonService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<SeasonResource>> GetAllSeasonsBySerieIdAsync(int serieId)
    {
        var seasons =  await _seasonService.ListBySerieIdAsync(serieId);

        var resources = _mapper.Map<IEnumerable<Season>, IEnumerable<SeasonResource>>(seasons);

        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostSeasonsBySerieIdAsync(int serieId, [FromBody] SaveSeasonResource resource)
    {

        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var season = _mapper.Map<SaveSeasonResource, Season>(resource);

        var result = await _seasonService.SaveBySerieIdAsync(season, serieId);

        if (!result.Success)
            return BadRequest(result.Message);

        var seasonResource = _mapper.Map<Season, SeasonResource>(result.Resource);

        return Ok(seasonResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id,int serieId, [FromBody] SaveSeasonResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var season = _mapper.Map<SaveSeasonResource, Season>(resource);
        var result = await _seasonService.UpdateBySerieIdAsync(id,season,serieId);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var seasonResource = _mapper.Map<Season, SeasonResource>(result.Resource);

        return Ok(seasonResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _seasonService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var seasonResource = _mapper.Map<Season, SeasonResource>(result.Resource);

        return Ok(seasonResource);
    }
}