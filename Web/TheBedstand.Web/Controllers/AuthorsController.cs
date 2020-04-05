namespace TheBedstand.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Common;
    using TheBedstand.Data;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Authors;
    using TheBedstand.Web.ViewModels.Authors;

    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly ApplicationDbContext db;

        public AuthorsController(IAuthorsService authorsService, ICloudinaryService cloudinaryService)
        {
            this.authorsService = authorsService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AuthorInputModel();
            this.AttachAuthorsForSelectListToInputModel(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.AttachAuthorsForSelectListToInputModel(input);
                return this.View(input);
            }

            string imageUrl = null;

            if (input.Image != null)
            {
                imageUrl = await this.cloudinaryService
                .UploadPhotoAsync(input.Image, $"{input.PersonalName + " " + input.Surname}", GlobalConstants.CloudFolderForAuthorsPhotos);
            }

            await this.authorsService.CreateAsync(input, imageUrl);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult All()
        {
            var model = this.authorsService.GetAuthorBasicInfo();
            return this.View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var author = this.authorsService.GetById(id);
            return this.View(author);
        }

        private AuthorInputModel AttachAuthorsForSelectListToInputModel(AuthorInputModel input)
        {
            input.AuthorsForSelectList = this.authorsService.GetAuthorBasicInfo();

            return input;
        }
    }
}
