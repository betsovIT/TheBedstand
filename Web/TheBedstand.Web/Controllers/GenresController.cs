namespace TheBedstand.Web.Controllers
{
    using System.Threading.Tasks;

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

        public IActionResult All()
        {
            var viewModel = this.genresService.GetAll();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateGenreInputModel();

            return this.View(model);
        }

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
    }
}
