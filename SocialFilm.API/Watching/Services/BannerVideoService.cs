using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Services;

public class BannerVideoService:IBannerVideoService
{
    private readonly IBannerVideoRepository _bannerVideoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BannerVideoService(IBannerVideoRepository bannerVideoRepository, IUnitOfWork unitOfWork)
    {
        _bannerVideoRepository = bannerVideoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BannerVideo>> ListAsync()
    {
        return await _bannerVideoRepository.ListAsync();
    }

    public async Task<BannerVideoResponse> SaveAsync(BannerVideo bannerVideo)
    {
        try
        {
            await _bannerVideoRepository.AddAsync(bannerVideo);
            await _unitOfWork.CompleteAsync();
            return new BannerVideoResponse(bannerVideo);
        }
        catch (Exception e)
        {
            return new BannerVideoResponse($"An error occurred while saving the bannerVideo: {e.Message}");
        }
    }

    public async Task<BannerVideoResponse> UpdateAsync(int bannerVideoId, BannerVideo bannerVideo)
    {
        var existingBannerVideo = await _bannerVideoRepository.FindByIdAsync(bannerVideoId);

        if (existingBannerVideo == null)
            return new BannerVideoResponse("BannerVideo not found");

        existingBannerVideo.Banner = bannerVideo.Banner;
        existingBannerVideo.Billboard = bannerVideo.Billboard;

        try
        {
            _bannerVideoRepository.Update(existingBannerVideo);
            await _unitOfWork.CompleteAsync();

            return new BannerVideoResponse(existingBannerVideo);
        }
        catch (Exception e)
        {
            return new BannerVideoResponse($"An error occurred while updating the BannerVideo: {e.Message}");
        }
    }

    public async Task<BannerVideoResponse> DeleteAsync(int bannerVideoId)
    {
        var existingBannerVideo = await _bannerVideoRepository.FindByIdAsync(bannerVideoId);

        if (existingBannerVideo == null)
            return new BannerVideoResponse("BannerVideo not found");

        try
        {
            _bannerVideoRepository.Remove(existingBannerVideo);
            _unitOfWork.CompleteAsync();
            
            return new BannerVideoResponse(existingBannerVideo);
        }
        catch (Exception e)
        {
            return new BannerVideoResponse($"An error occurred while deleting the BannerVideo: {e.Message}");
        }
    }
}