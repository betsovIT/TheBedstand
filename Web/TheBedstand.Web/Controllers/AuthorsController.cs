namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Common;
    using TheBedstand.Common.Helpers;
    using TheBedstand.Data.Models.Enums;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Authors;
    using TheBedstand.Web.ViewModels.Authors;
    using TheBedstand.Web.ViewModels.Books;

    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly ICloudinaryService cloudinaryService;

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
                var result = await this.cloudinaryService
                .UploadPhotoAsync(input.Image, $"{input.PersonalName + " " + input.Surname}", GlobalConstants.CloudFolderForAuthorsPhotos);

                imageUrl = result?.PublicId;
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

            var viewModel = new AuthorDetailsViewModel
            {
                Biography = author.Biography,
                Country = EnumDescriptionHelper.GetEnumValueDescription<Country>(author.Country),
                DateOfBirth = author.DateOfBirth,
                ImageUrl = author.ImageUrl,
                Surname = author.Surname,
                PersonalName = author.PersonalName,
                Id = author.Id,
                PseudonymForAuthor = author.PseudonymFor == null ? "N/A" : NameHelper.GetFullName(author.PseudonymFor.PersonalName, author.PseudonymFor.Surname),
                Genres = author.Books.SelectMany(b => b.BookGenres).Select(bg => bg.Genre?.Name).Distinct().ToArray(),
                Books = author.Books.Select(b => new BookAuthorPageViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverUrl = b.CoverUrl,
                }).ToList(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = this.authorsService.GetById(id);

            var authorInputModel = new AuthorInputModel
            {
                Id = id,
                Biography = author.Biography,
                Country = author.Country,
                DateOfBirth = author.DateOfBirth,
                PersonalName = author.PersonalName,
                Surname = author.Surname,
                PseudonymForId = author.PseudonymForId,
            };

            var authorWithAttachedSelectList = this.AttachAuthorsForSelectListToInputModel(authorInputModel);

            return this.View(authorWithAttachedSelectList);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(this.AttachAuthorsForSelectListToInputModel(input));
            }

            var author = this.authorsService.GetById(input.Id);

            if (input.Image != null)
            {
                await this.cloudinaryService.Delete(author.ImageUrl);

                var result = await this.cloudinaryService
                .UploadPhotoAsync(input.Image, $"{input.PersonalName + " " + input.Surname}", GlobalConstants.CloudFolderForAuthorsPhotos);

                author.ImageUrl = result?.PublicId;
            }

            author.Biography = input.Biography;
            author.Country = input.Country;
            author.DateOfBirth = input.DateOfBirth;
            author.PersonalName = input.PersonalName;
            author.Surname = input.Surname;
            author.PseudonymForId = input.PseudonymForId != null ? (int?)this.authorsService.GetById((int)input.PseudonymForId).Id : null;

            await this.authorsService.PersistEditedAuthorToDb();

            return this.RedirectToAction("Details", new { id = author.Id });
        }

        private AuthorInputModel AttachAuthorsForSelectListToInputModel(AuthorInputModel input)
        {
            input.AuthorsForSelectList = this.authorsService.GetAuthorBasicInfo();

            return input;
        }
    }
}
