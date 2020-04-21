namespace TheBedstand.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Common;
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
                var result = await this.cloudinaryService
                .UploadPhotoAsync(input.Cover, $"{input.Title + " _cover"}", GlobalConstants.CloudFolderForBookCovers);

                imageUrl = result?.PublicId;
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

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var book = this.booksService.GetByIdAsDbModel(id);
            var model = new BookInputModel()
            {
                Annotation = book.Annotation,
                ISBN = book.Id,
                AuthorId = book.Author.Id,
                PageCount = book.PageCount,
                PublishedOn = book.PublishedOn,
                Title = book.Title,
                GenreIds = book.BookGenres.Select(x => x.GenreId).ToArray(),
            };

            this.AttachSelectListsToBookInputModel(model);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookInputModel input)
        {
            var dbBook = this.booksService.GetByIdAsDbModel(input.ISBN);

            this.ModelState.Remove("GenreIds");

            if (!this.ModelState.IsValid)
            {
                this.AttachSelectListsToBookInputModel(input);
                return this.View(input);
            }

            if (!this.booksService.All().Any(x => x.Id == input.ISBN))
            {
                this.AttachSelectListsToBookInputModel(input);
                return this.View(input);
            }

            if (input.Cover != null)
            {
                await this.cloudinaryService.Delete(dbBook.CoverUrl);
                var uploadResult = await this.cloudinaryService.UploadPhotoAsync(input.Cover, $"{input.Title + " _cover"}", GlobalConstants.CloudFolderForBookCovers);

                if (uploadResult.PublicId != null)
                {
                    dbBook.CoverUrl = uploadResult.PublicId;
                }
            }

            dbBook.Annotation = input.Annotation;
            dbBook.PageCount = input.PageCount;
            dbBook.PublishedOn = input.PublishedOn;
            dbBook.Title = input.Title;
            dbBook.AuthorId = input.AuthorId;

            await this.booksService.PersistBookToDb(dbBook, input.GenreIds);

            return this.RedirectToAction("Details", new { id = dbBook.Id });
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
