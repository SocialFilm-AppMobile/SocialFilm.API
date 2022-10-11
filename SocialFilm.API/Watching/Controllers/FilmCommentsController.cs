using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialFilm.API.Shared.Extensions;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Resources;

namespace SocialFilm.API.Watching.Controllers;
[ApiController]
[Route("api/v1/films/{filmId}/comments")]

public class FilmCommentsController:ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;


    public FilmCommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CommentResource>> GetAllCommentsByFilmIdAsync(int filmId)
    {
        var comments = await _commentService.ListByFilmIdAsync(filmId);
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostCommentByFilmIdAsync(int filmId, [FromBody] SaveCommentResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var comment = _mapper.Map<SaveCommentResource, Comment>(resource);

        var result = await _commentService.SaveAsync(comment,filmId);

        if (!result.Success)
            return BadRequest(result.Message);

        var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
        return Ok(commentResource);
    }

}