namespace TheBedstand.Services.Data
{
    using System.IO;
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

        public async Task<string> UploadPhotoAsync(IFormFile file, string fileName, string folder)
        {
            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            memoryStream.Position = 0;

            ImageUploadParams uploadparams = new ImageUploadParams()
            {
                Folder = folder,
                File = new FileDescription(fileName, memoryStream),
            };

            var uploadResult = this.utility.UploadAsync(uploadparams).GetAwaiter().GetResult();

            return uploadResult?.SecureUri?.AbsoluteUri;
        }
    }
}
