namespace TheBedstand.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Common;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Genres;

    public class GenresController : BaseController
    {
        private readonly IGenresService genresService;
        private readonly ICloudinaryService cloudinaryService;

        public GenresController(IGenresService genresService, ICloudinaryService cloudinaryService)
        {
            this.genresService = genresService;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize]
        [HttpGet]
        public IActionResult All()
        {
            var viewModel = this.genresService.GetAll();

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateGenreInputModel();

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var result = await this.cloudinaryService.UploadPhotoAsync(input.Image, $"{input.Name}", GlobalConstants.CloudFolderForGenrePhotos);

            await this.genresService.CreateAsync(input.Name, input.Description, result?.Uri.AbsoluteUri);

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var genre = this.genresService.GetbyId(id);
            var model = new CreateGenreInputModel { Description = genre.Description, Name = genre.Name };

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Edit(CreateGenreInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var genre = this.genresService.GetbyId(input.Id);

            if (input.Image != null)
            {
                var uploadResult = await this.cloudinaryService.UploadPhotoAsync(input.Image, $"{input.Name}", GlobalConstants.CloudFolderForGenrePhotos);

                if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    await this.cloudinaryService.Delete(genre.ImageId);

                    genre.ImageId = uploadResult.PublicId;
                    genre.ImageUrl = uploadResult.Uri.AbsoluteUri;
                }
                else
                {
                    this.TempData["ImageUploadError"] = "Failed to upload image.";
                    return this.View(input);
                }
            }

            genre.Name = input.Name;
            genre.Description = input.Description;

            await this.genresService.PersistChanges();

            return this.RedirectToAction("All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.genresService.Delete(id);

            if (result)
            {
                this.TempData["result"] = "Successfuly deleted the genre.";
                return this.RedirectToAction("All");
            }
            else
            {
                this.TempData["result"] = "Failed to delete the genre.";
                return this.RedirectToAction("All");
            }
        }
    }
}
