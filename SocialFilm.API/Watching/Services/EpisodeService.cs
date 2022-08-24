using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Domain.Services.Communication;
using SocialFilm.API.Watching.Persistence.Repositories;

namespace SocialFilm.API.Watching.Services;

public class EpisodeService:IEpisodeService
{
    private readonly IEpisodeRepository _episodeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISeasonRepository _seasonRepository;
    private readonly IVideoRepository _videoRepository;

    public EpisodeService(IEpisodeRepository episodeRepository, IUnitOfWork unitOfWork, ISeasonRepository seasonRepository, IVideoRepository videoRepository)
    {
        _episodeRepository = episodeRepository;
        _unitOfWork = unitOfWork;
        _seasonRepository = seasonRepository;
        _videoRepository = videoRepository;
    }

    public async Task<IEnumerable<Episode>> ListAsync()
    {
        return await _episodeRepository.ListAsync();
    }

    public async Task<IEnumerable<Episode>> ListBySeasonIdAsync(int seasonId)
    {
        return await _episodeRepository.FindBySeasonIdAsync(seasonId);
    }

    public async Task<EpisodeResponse> SaveAsync(Episode episode)
    {
        //var existingSeason = await _seasonRepository.FindByIdAsync(episode.SeasonId);

        //if (existingSeason == null)
            //return new EpisodeResponse("Invalid Season");

            var existingEpisodeWithTitle = await _episodeRepository.FindByTitleAsync(episode.Title);
        
        if (existingEpisodeWithTitle != null)
            return new EpisodeResponse("Episode title already exists.");
        
        try
        {
            await _episodeRepository.AddAsync(episode);
            await _unitOfWork.CompleteAsync();
            return new EpisodeResponse(episode);
        }
        catch (Exception e)
        {
            return new EpisodeResponse($"An error occurred while saving the episode: {e.Message}");
        }
    }

    public async Task<EpisodeResponse> SaveBySeasonIdAsync(int seasonId, Episode episode)
    {
        var existingSeason = await _seasonRepository.FindByIdAsync(seasonId);

        if (existingSeason == null)
            return new EpisodeResponse("Invalid Season");
        
        episode.SeasonId = seasonId;

        var existingEpisodeWithTitle = await _episodeRepository.FindByTitleAsync(episode.Title);
        
        if (existingEpisodeWithTitle != null)
            return new EpisodeResponse("Episode title already exists.");
        
        try
        {
            await _episodeRepository.AddAsync(episode);
            await _unitOfWork.CompleteAsync();
            return new EpisodeResponse(episode);
        }
        catch (Exception e)
        {
            return new EpisodeResponse($"An error occurred while saving the episode: {e.Message}");
        }
    }

    public async Task<EpisodeResponse> UpdateAsync(int episodeId, Episode episode)
    {
        var existingEpisode = await _episodeRepository.FindByIdAsync(episodeId);

        if (existingEpisode == null)
            return new EpisodeResponse("Episode not found");
        
        var existingSeason = await _seasonRepository.FindByIdAsync(episode.SeasonId);
        
        if (existingSeason == null)
            return new EpisodeResponse("Invalid Season");
        
        var existingVideo = await _videoRepository.FindByIdAsync(episode.VideoId);
        
        if (existingVideo == null)
            return new EpisodeResponse("Invalid Video");
        
        var existingEpisodeWithTitle = await _episodeRepository.FindByTitleAsync(episode.Title);
        
        if (existingEpisodeWithTitle != null && existingEpisodeWithTitle.Id != existingEpisode.Id)
            return new EpisodeResponse("Episode title already exists.");

        existingEpisode.Title = episode.Title;
        existingEpisode.Synopsis = episode.Synopsis;
        existingEpisode.Video.VideoUrl = episode.Video.VideoUrl;
        existingEpisode.Season = episode.Season;

        try
        {
            _episodeRepository.Update(existingEpisode);
            await _unitOfWork.CompleteAsync();
            
            return new EpisodeResponse(existingEpisode);
        }
        catch (Exception e)
        {
            return new EpisodeResponse($"An error occurred while updating the episode: {e.Message}");
        }

    }

    public async Task<EpisodeResponse> UpdateBySeasonIdAsync(int seasonId, int episodeId, Episode episode)
    {
        var existingEpisode = await _episodeRepository.FindByIdAsync(episodeId);

        if (existingEpisode == null)
            return new EpisodeResponse("Episode not found");
        
        var existingSeason = await _seasonRepository.FindByIdAsync(seasonId);
        
        if (existingSeason == null)
            return new EpisodeResponse("Invalid Season");

        var existingEpisodeWithTitle = await _episodeRepository.FindByTitleAsync(episode.Title);
        
        if (existingEpisodeWithTitle != null && existingEpisodeWithTitle.Id != existingEpisode.Id)
            return new EpisodeResponse("Episode title already exists.");

        existingEpisode.Title = episode.Title;
        existingEpisode.Synopsis = episode.Synopsis;
        existingEpisode.Video.VideoUrl = episode.Video.VideoUrl;
        existingEpisode.SeasonId = seasonId;

        try
        {
            _episodeRepository.Update(existingEpisode);
            await _unitOfWork.CompleteAsync();
            
            return new EpisodeResponse(existingEpisode);
        }
        catch (Exception e)
        {
            return new EpisodeResponse($"An error occurred while updating the episode: {e.Message}");
        }
    }

    public async Task<EpisodeResponse> DeleteAsync(int episodeId)
    {
        var existingEpisode = await _episodeRepository.FindByIdAsync(episodeId);

        if (existingEpisode == null)
            return new EpisodeResponse("Episode not found");
        try
        {
            _episodeRepository.Remove(existingEpisode);
            await _unitOfWork.CompleteAsync();

            return new EpisodeResponse(existingEpisode);
            
        }
        catch (Exception e)
        {
            return new EpisodeResponse($"An error occurred while deleting the episode: {e.Message}");
        }
    }
}