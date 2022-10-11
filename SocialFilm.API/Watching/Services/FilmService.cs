using SocialFilm.API.Shared.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Models;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Services;

public class FilmService:IFilmService
{
    private readonly IFilmRepository _filmRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IBannerVideoRepository _bannerVideoRepository;
    

    public FilmService(IFilmRepository filmRepository, IUnitOfWork unitOfWork, ICategoryRepository categoryRepository, IBannerVideoRepository bannerVideoRepository)
    {
        _filmRepository = filmRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _bannerVideoRepository = bannerVideoRepository;
    }

    public async Task<IEnumerable<Film>> ListAsync()
    {
        return await _filmRepository.ListAsync();
    }

    public async Task<IEnumerable<Film>> ListByUserIdAsync(int userId)
    {
        return await _filmRepository.FindByUserIdAsync(userId);
    }

    public async Task<FilmResponse> GetAsync(int filmId)
    {
        var existingFilm = await _filmRepository.FindByIdAsync(filmId);

        if (existingFilm == null)
            return new FilmResponse("Film not found");

        try
        {
            await _unitOfWork.CompleteAsync();
            return new FilmResponse(existingFilm);
        }
        catch (Exception e)
        {
            return new FilmResponse($"An error occurred while geting the film: {e.Message}");
        }
    }

    public async Task<IEnumerable<Film>> ListByCategoryIdAsync(int categoryId)
    {
        return await _filmRepository.FindByCategoryIdAsync(categoryId);
    }
    
    public async Task<FilmResponse> SaveAsync(Film film)
    {
        var existingCategory = await _categoryRepository.FindByIdAsync(film.CategoryId);
        
        if (existingCategory == null)
            return new FilmResponse("Invalid Category");

        var existingFilmWithTitle = await _filmRepository.FindByTitleAsync(film.Title);
        
        if (existingFilmWithTitle != null)
            return new FilmResponse("Film title already exists.");
        
        try
        {
            await _filmRepository.AddAsync(film);
            await _unitOfWork.CompleteAsync();
            return new FilmResponse(film);

        }
        catch (Exception e)
        {
            return new FilmResponse($"An error occurred while saving the film: {e.Message}");
        }
    }

    public async Task<FilmResponse> UpdateAsync(int filmId, Film film)
    {
        var existingFilm = await _filmRepository.FindByIdAsync(filmId);

        var existingBanner = await _bannerVideoRepository.FindByFilmIdAsync(filmId);
        
        if (existingFilm == null)
            return new FilmResponse("Film not found.");
        
        var existingCategory = await _categoryRepository.FindByIdAsync(film.CategoryId);
        if (existingCategory == null)
            return new FilmResponse("Invalid Category");
        

        
        var existingFilmWithTitle = await _filmRepository.FindByTitleAsync(film.Title);
        
        if (existingFilmWithTitle != null && existingFilmWithTitle.Id != existingFilm.Id)
            return new FilmResponse("Film title already exists.");
        
        existingFilm.Title = film.Title;
        existingFilm.Synopsis = film.Synopsis;
        existingFilm.VideoURL = film.VideoURL;
        existingFilm.UserId = film.UserId;
       
        existingFilm.CategoryId= film.CategoryId;

        existingBanner.Billboard = film.BannerVideo.Billboard;
        existingBanner.Banner = film.BannerVideo.Banner;

        try
        {
            _bannerVideoRepository.Update(existingBanner);
            _filmRepository.Update(existingFilm);
            
            await _unitOfWork.CompleteAsync();
            
            return new FilmResponse(existingFilm);

        }
        catch (Exception e)
        {
            return new FilmResponse($"An error occurred while updating the film: {e.Message}");
        }
    }

    public async Task<FilmResponse> DeleteAsync(int filmId)
    {
        var existingFilm = await _filmRepository.FindByIdAsync(filmId);

        if (existingFilm == null)
            return new FilmResponse("Film not found");

        try
        {
            _filmRepository.Remove(existingFilm);
            await _unitOfWork.CompleteAsync();

            return new FilmResponse(existingFilm);
        }
        catch (Exception e)
        {
            return new FilmResponse($"An error occurred while deleting the film: {e.Message}");
        }
    }
}