namespace TheBedstand.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Common;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Books;
    using TheBedstand.Web.ViewModels.Authors;
    using TheBedstand.Web.ViewModels.Books;
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

        [Authorize]
        [HttpGet]
        public IActionResult All(int? page)
        {
            var books = this.booksService.All();
            var pager = new Pager(books.Count(), page, 5);

            var model = new BooksIndexViewModel
            {
                Items = books.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
            };

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new BookInputModel();

            this.AttachSelectListsToBookInputModel(model);

            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Create(BookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.AttachSelectListsToBookInputModel(input);
                return this.View(input);
            }

            if (input.Cover != null)
            {
                var result = await this.cloudinaryService
                .UploadPhotoAsync(input.Cover, $"{input.Title + " _cover"}", GlobalConstants.CloudFolderForBookCovers);

                await this.booksService.Create(input, result);
            }

            return this.RedirectToAction("All");
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetByGenre(int id, int? page)
        {
            this.ViewData["genreId"] = id;

            var books = this.booksService.GetByGenre(id);
            var pager = new Pager(books.Count(), page, 5);

            var model = new BooksIndexViewModel
            {
                Items = books.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                Pager = pager,
            };
            return this.View("ByGenre", model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Details(string id)
        {
            var model = this.booksService.GetById(id);

            return this.View(model);
        }

        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
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

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            if (await this.booksService.DeleteBook(id))
            {
                return this.RedirectToAction("All", "Books");
            }
            else
            {
                return this.RedirectToAction("Details", new { id = id });
            }
        }

        private BookInputModel AttachSelectListsToBookInputModel(BookInputModel input)
        {
            var authorsForSelectList = this.authorsService.GetAuthorBasicInfo().Select(x => new AuthorBasicInfoModel
            {
                Id = x.Id,
                PersonalName = x.PersonalName,
                Surname = x.Surname,
            });

            var genresForSelectList = this.genresService.GetGenresForSelectList().Select(x => new GenreForSelectListModel
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
