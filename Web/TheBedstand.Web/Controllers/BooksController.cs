namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TheBedstand.Common.Helpers;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Books;

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
            var authorsSelectList = this.authorsService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = NameHelper.GetFullName(x.PersonalName, x.Surname),
            });

            var genresSelectList = this.genresService.GetGenresForSelectList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            });

            this.ViewData.Add("Authors", authorsSelectList);
            this.ViewData.Add("Genres", genresSelectList);

            return this.View();
        }

        [HttpPost]
        public IActionResult Create(BookInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.RedirectToAction("Index", "Home");
        }
    }
}
