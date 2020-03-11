namespace TheBedstand.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Genres;

    public class GenresController : BaseController
    {
        private readonly IGenresService genresService;

        public GenresController(IGenresService genresService)
        {
            this.genresService = genresService;
        }

        public IActionResult All()
        {
            var viewModel = this.genresService.GetAll();

            return this.View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGenreInputModel input)
        {
            await this.genresService.CreateAsync(input);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
