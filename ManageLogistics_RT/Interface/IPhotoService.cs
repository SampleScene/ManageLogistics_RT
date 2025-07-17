using CloudinaryDotNet.Actions;

namespace ManageLogistics_RT.Interface
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(string publicId);
        Task<ImageUploadResult> AddPhotoToUserProfileAsync(IFormFile file);
    }
}
