using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Services;

public class SeasonService:ISeasonService
{
    private readonly ISeasonRepository _seasonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISerieRepository _serieRepository;
    private readonly IEpisodeRepository _episodeRepository;

    public SeasonService(ISeasonRepository seasonRepository, IUnitOfWork unitOfWork, ISerieRepository serieRepository, IEpisodeRepository episodeRepository)
    {
        _seasonRepository = seasonRepository;
        _unitOfWork = unitOfWork;
        _serieRepository = serieRepository;
        _episodeRepository = episodeRepository;
    }

    public async Task<IEnumerable<Season>> ListAsync()
    {
        return await _seasonRepository.ListAsync();
    }

    public async Task<IEnumerable<Season>> ListBySerieIdAsync(int serieId)
    {
        return await _seasonRepository.FindBySerieIdAsync(serieId);
    }
    
    public async Task<SeasonResponse> SaveAsync(Season season)
    {
        var existingSerie = await _serieRepository.FindByIdAsync(season.SerieId);

        if (existingSerie == null)
            return new SeasonResponse("Invalid Serie");
        
        var existingSeasonWithTitle = await _seasonRepository.FindByTitleAsync(season.Title);

        if (existingSeasonWithTitle != null)
            return new SeasonResponse("Season title already exists.");
        
        try
        {
            await _seasonRepository.AddAsync(season);
            await _unitOfWork.CompleteAsync();
            return new SeasonResponse(season);

        }
        catch (Exception e)
        {
            // Error Handling
            return new SeasonResponse($"An error occurred while saving the season: {e.Message}");
        }
    }
    
    public async Task<SeasonResponse> SaveBySerieIdAsync(Season season, int serieId)
    {
        var existingSerie = await _serieRepository.FindByIdAsync(serieId);

        if (existingSerie == null)
            return new SeasonResponse("Invalid Serie");

        season.SerieId = serieId;
        
        var existingSeasonWithTitle = await _seasonRepository.FindByTitleAsync(season.Title);

        if (existingSeasonWithTitle != null)
            return new SeasonResponse("Season title already exists.");
        
        try
        {
            await _seasonRepository.AddAsync(season);
            await _unitOfWork.CompleteAsync();
            return new SeasonResponse(season);

        }
        catch (Exception e)
        {
            // Error Handling
            return new SeasonResponse($"An error occurred while saving the season: {e.Message}");
        }
    }

    public async Task<SeasonResponse> UpdateAsync(int seasonId, Season season)
    {
        var existingSeason = await _seasonRepository.FindByIdAsync(seasonId);

        if (existingSeason == null)
            return new SeasonResponse("Season not found");

        var existingSerie = await _serieRepository.FindByIdAsync(season.SerieId);

        if (existingSerie == null)
            return new SeasonResponse("Invalid Serie");

        var existingEpisode = await _episodeRepository.FindByIdAsync(season.EpisodeId);

        if (existingEpisode == null)
            return new SeasonResponse("Invalid Episode");

        existingSeason.Title = season.Title;
        existingSeason.Synopsis = season.Synopsis;
        existingSeason.Serie = season.Serie;
        existingSeason.Episodes = season.Episodes;

        try
        {
            _seasonRepository.Update(existingSeason);
            _unitOfWork.CompleteAsync();
            
            return new SeasonResponse(existingSeason);
        }
        catch (Exception e)
        {
            return new SeasonResponse($"An error occurred while updating the season: {e.Message}");
        }
    }

    public async Task<SeasonResponse> UpdateBySerieIdAsync(int seasonId, Season season, int serieId)
    {
        var existingSeason = await _seasonRepository.FindByIdAsync(seasonId);

        if (existingSeason == null)
            return new SeasonResponse("Season not found");

        var existingSerie = await _serieRepository.FindByIdAsync(serieId);

        if (existingSerie == null)
            return new SeasonResponse("Invalid Serie");

        existingSeason.Title = season.Title;
        existingSeason.Synopsis = season.Synopsis;
        existingSeason.SerieId = serieId;
        existingSeason.Episodes = season.Episodes;

        try
        {
            _seasonRepository.Update(existingSeason);
            _unitOfWork.CompleteAsync();
            
            return new SeasonResponse(existingSeason);
        }
        catch (Exception e)
        {
            return new SeasonResponse($"An error occurred while updating the season: {e.Message}");
        }
    }

    public async Task<SeasonResponse> DeleteAsync(int seasonId)
    {
        var existingSeason = await _seasonRepository.FindByIdAsync(seasonId);

        if (existingSeason == null)
            return new SeasonResponse("Season not found");
        try
        {
            _seasonRepository.Remove(existingSeason);
            await _unitOfWork.CompleteAsync();

            return new SeasonResponse(existingSeason);
            
        }
        catch (Exception e)
        {
            return new SeasonResponse($"An error occurred while deleting the season: {e.Message}");
        }
    }

    public async Task<SeasonResponse> DeleteBySerieIdAsync(int seasonId, int serieId)
    {
        var existingSeason = await _seasonRepository.FindByIdAsync(seasonId);

        if (existingSeason == null)
            return new SeasonResponse("Season not found");
        try
        {
            _seasonRepository.Remove(existingSeason);
            await _unitOfWork.CompleteAsync();

            return new SeasonResponse(existingSeason);
            
        }
        catch (Exception e)
        {
            return new SeasonResponse($"An error occurred while deleting the season: {e.Message}");
        }
    }
}