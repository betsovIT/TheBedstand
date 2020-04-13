namespace TheBedstand.Services
{
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile file, string fileName, string folder);

        Task<string> GetUrlById(string publicId);

        Task Delete(string publicId);
    }
}
