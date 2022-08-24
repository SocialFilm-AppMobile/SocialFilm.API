using SocialFilm.API.Shared.Domain.Model;
using SocialFilm.API.Watching.Domain.Repositories;
using SocialFilm.API.Watching.Domain.Services;
using SocialFilm.API.Watching.Domain.Services.Communication;

namespace SocialFilm.API.Watching.Services;

public class VideoService : IVideoService
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public VideoService(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Video>> ListAsync()
    {
        return await _videoRepository.ListAsync();
    }

    public async Task<VideoResponse> SaveAsync(Video video)
    {
        try
        {
            await _videoRepository.AddAsync(video);
            await _unitOfWork.CompleteAsync();
            return new VideoResponse(video);
        }
        catch (Exception e)
        {
            return new VideoResponse($"An error occurred while saving the video: {e.Message}");
        }
    }

    public async Task<VideoResponse> UpdateAsync(int videoId, Video video)
    {
        var existingVideo = await _videoRepository.FindByIdAsync(videoId);

        if (existingVideo == null)
            return new VideoResponse("Video not found");

        existingVideo.VideoUrl = video.VideoUrl;

        try
        {
            _videoRepository.Update(existingVideo);
            await _unitOfWork.CompleteAsync();

            return new VideoResponse(existingVideo);
        }
        catch (Exception e)
        {
            return new VideoResponse($"An error occurred while updating the video: {e.Message}");
        }
    }

    public async Task<VideoResponse> DeleteAsync(int videoId)
    {
        var existingVideo = await _videoRepository.FindByIdAsync(videoId);

        if (existingVideo == null)
            return new VideoResponse("Video not found");

        try
        {
            _videoRepository.Remove(existingVideo);
            _unitOfWork.CompleteAsync();
            
            return new VideoResponse(existingVideo);
        }
        catch (Exception e)
        {
            return new VideoResponse($"An error occurred while deleting the video: {e.Message}");
        }
    }
}