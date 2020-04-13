namespace TheBedstand.Services.Data
{
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary utility;

        public CloudinaryService(Cloudinary utility)
        {
            this.utility = utility;
        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile file, string fileName, string folder)
        {
            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            memoryStream.Position = 0;

            ImageUploadParams uploadparams = new ImageUploadParams()
            {
                Folder = folder,
                File = new FileDescription(fileName, memoryStream),
            };

            var uploadResult = await this.utility.UploadAsync(uploadparams);

            return uploadResult;
        }

        public async Task<string> GetUrlById(string publicId)
        {
            if (string.IsNullOrEmpty(publicId))
            {
                return null;
            }

            var result = await this.utility.GetResourceAsync(publicId);

            return result?.Url;
        }

        public async Task Delete(string publicId)
        {
            var result = await this.utility.DeleteResourcesAsync(new string[] { publicId });
        }
    }
}
