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
        private readonly IRepository<BookGenre> bookGenreRepository;

        public GenresService(IDeletableEntityRepository<Genre> genresRepository, IRepository<BookGenre> bookGenreRepository)
        {
            this.genresRepository = genresRepository;
            this.bookGenreRepository = bookGenreRepository;
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

        public async Task<bool> Delete(int id)
        {
            var genre = this.genresRepository.All().FirstOrDefault(x => x.Id == id);

            if (genre == null)
            {
                return false;
            }
            else
            {
                this.genresRepository.Delete(genre);
                var bookgenres = this.bookGenreRepository.All().Where(x => x.GenreId == genre.Id);

                foreach (var bookgenre in bookgenres)
                {
                    this.bookGenreRepository.Delete(bookgenre);
                }

                await this.bookGenreRepository.SaveChangesAsync();
                await this.genresRepository.SaveChangesAsync();

                return true;
            }
        }

        public AllGenresViewModel GetAll()
        {
            var result = new AllGenresViewModel();

            result.Genres = this.genresRepository.All().Select(x => new GenreInfoViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                ImageUrl = x.ImageUrl,
            });

            return result;
        }

        public Genre GetbyId(int id)
        {
            return this.genresRepository.All().FirstOrDefault(x => x.Id == id);
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

        public async Task PersistChanges()
        {
            await this.genresRepository.SaveChangesAsync();
        }
    }
}
