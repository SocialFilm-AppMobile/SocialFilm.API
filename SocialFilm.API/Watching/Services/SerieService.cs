using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Services;

public class SerieService:ISerieService
{
    private readonly ISerieRepository _serieRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISeasonRepository _seasonRepository;
    private readonly ICategoryRepository _categoryRepository;

    public SerieService(ISerieRepository serieRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, ISeasonRepository seasonRepository)
    {
        _serieRepository = serieRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _seasonRepository = seasonRepository;
    }

    public async Task<IEnumerable<Serie>> ListAsync()
    {
        return await _serieRepository.ListAsync();
    }

    public async Task<IEnumerable<Serie>> ListByCategoryIdAsync(int categoryId)
    {
        return await _serieRepository.FindByCategoryIdAsync(categoryId);
    }

    public async Task<SerieResponse> SaveAsync(Serie serie)
    {
        var existingCategory = await _categoryRepository.FindByIdAsync(serie.CategoryId);

        if (existingCategory == null)
            return new SerieResponse("Invalid Category");
        
        /*var existingSeason = await _seasonRepository.FindByIdAsync(serie.SeasonId);

        if (existingSeason == null)
            return new SerieResponse("Invalid Season");*/

        var existingSerieWithTitle = await _serieRepository.FindByTitleAsync(serie.Title);

        if (existingSerieWithTitle != null)
            return new SerieResponse("Serie title already exists.");

        try
        {
            await _serieRepository.AddAsync(serie);
            await _unitOfWork.CompleteAsync();
            
            return new SerieResponse(serie);

        }
        catch (Exception e)
        {
            return new SerieResponse($"An error occurred while saving the serie: {e.Message}");
        }
    }

    public async Task<SerieResponse> UpdateAsync(int serieId, Serie serie)
    {
        var existingSerie = await _serieRepository.FindByIdAsync(serieId);

        if (existingSerie == null)
            return new SerieResponse("Serie not found");
        
        var existingCategory = await _categoryRepository.FindByIdAsync(serie.CategoryId);

        if (existingCategory == null)
            return new SerieResponse("Invalid Category");

        var existingSerieWithTitle = await _serieRepository.FindByTitleAsync(serie.Title);

        if (existingSerieWithTitle != null && existingSerieWithTitle.Id != existingSerie.Id)
            return new SerieResponse("Serie title already exists.");
        
        existingSerie.Title = serie.Title;
        existingSerie.Synopsis = serie.Synopsis;
        existingSerie.Seasons = serie.Seasons;
        existingSerie.CategoryId = serie.CategoryId;
        
        try
        {
            _serieRepository.Update(existingSerie);
            await _unitOfWork.CompleteAsync();
            
            return new SerieResponse(existingSerie);

        }
        catch (Exception e)
        {
            return new SerieResponse($"An error occurred while updating the serie: {e.Message}");
        }
    }

    public async Task<SerieResponse> DeleteAsync(int serieId)
    {
        var existingSerie = await _serieRepository.FindByIdAsync(serieId);

        if (existingSerie == null)
            return new SerieResponse("Serie not found");

        try
        {
            _serieRepository.Remove(existingSerie);
            await _unitOfWork.CompleteAsync();

            return new SerieResponse(existingSerie);
        }
        catch (Exception e)
        {
            return new SerieResponse($"An error occurred while deleting the serie: {e.Message}");
        }
    }
}