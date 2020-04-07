namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TheBedstand.Common;
    using TheBedstand.Common.Helpers;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Books;
    using TheBedstand.Web.ViewModels.Authors;
    using TheBedstand.Web.ViewModels.Genres;

    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IGenresService genresService;

        public BooksController(IBooksService booksService, IAuthorsService authorsService, ICloudinaryService cloudinaryService, IGenresService genresService)
        {
            this.booksService = booksService;
            this.authorsService = authorsService;
            this.cloudinaryService = cloudinaryService;
            this.genresService = genresService;
        }

        public IActionResult All()
        {
            var model = this.booksService.All();

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new BookInputModel();

            this.AttachSelectListsToBookInputModel(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.AttachSelectListsToBookInputModel(input);
                return this.View(input);
            }

            string imageUrl = null;

            if (input.Cover != null)
            {
                imageUrl = await this.cloudinaryService
                .UploadPhotoAsync(input.Cover, $"{input.Title + " _cover"}", GlobalConstants.CloudFolderForBookCovers);
            }

            await this.booksService.Create(input, imageUrl);

            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetByGenre(int id)
        {
            return this.View("All", this.booksService.GetByGenre(id));
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = this.booksService.GetById(id);

            return this.View(model);
        }

        private BookInputModel AttachSelectListsToBookInputModel(BookInputModel input)
        {
            var authorsForSelectList = this.authorsService.GetAuthorBasicInfo().Select(x => new AuthorBasicInfoModel
            {
                Id = x.Id,
                PersonalName = x.PersonalName,
                Surname = x.Surname,
            });

            var genresForSelectList = this.genresService.GetGenresForSelectList().Select(x => new GenreForSelectListViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            input.GenresForSelectList = genresForSelectList.ToArray();
            input.AuthorsForSelectList = authorsForSelectList.ToArray();

            return input;
        }
    }
}
