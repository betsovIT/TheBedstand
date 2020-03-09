namespace TheBedstand.Services.Data
{
    using System;
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

        public async Task CreateAsync(string name)
        {
            var genre = new Genre
            {
                Name = name,
            };

            await this.genresRepository.AddAsync(genre);
        }

        public AllGenresViewModel GetAll()
        {
            var result = new AllGenresViewModel();

            result.Genres = this.genresRepository.All().Select(x => new GenreInfoViewModel
            {
                Name = x.Name,
            });

            return result;
        }
    }
}
