namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.ViewModels.Genres;

    public class GenresService : IGenresService
    {
        private readonly IDeletableEntityRepository<Genre> genresRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository)
        {
            this.genresRepository = genresRepository;
        }

        public async Task CreateAsync(string name, string description, string imageUrl)
        {
            var genre = new Genre
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
            };

            await this.genresRepository.AddAsync(genre);
            await this.genresRepository.SaveChangesAsync();
        }

        public AllGenresViewModel GetAll()
        {
            var result = new AllGenresViewModel();

            result.Genres = this.genresRepository.All().Select(x => new GenreInfoViewModel
            {
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
            });

            return result;
        }

        public IEnumerable<GenreForSelectListModel> GetGenresForSelectList()
        {
            var result = this.genresRepository.All().Select(x => new GenreForSelectListModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            return result;
        }
    }
}
