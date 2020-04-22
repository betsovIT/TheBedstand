namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TheBedstand.Common.Helpers;
    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
    using TheBedstand.Data.Models.Enums;
    using TheBedstand.Web.InputModels.Authors;
    using TheBedstand.Web.ViewModels.Authors;
    using TheBedstand.Web.ViewModels.Books;

    public class AuthorsService : IAuthorsService
    {
        private readonly IDeletableEntityRepository<Author> authorRepository;

        public AuthorsService(IDeletableEntityRepository<Author> authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task CreateAsync(AuthorInputModel input, string imageUrl)
        {
            var author = new Author
            {
                PersonalName = input.PersonalName,
                Surname = input.Surname,
                Biography = input.Biography,
                DateOfBirth = input.DateOfBirth,
                Country = (Country)input.Country,
                PseudonymForId = input.PseudonymForId,
                ImageUrl = imageUrl,
            };

            await this.authorRepository.AddAsync(author);
            await this.authorRepository.SaveChangesAsync();
        }

        public IEnumerable<AuthorBasicInfoModel> GetAuthorBasicInfo()
        {
            var result = this.authorRepository
                .All()
                .Select(x => new AuthorBasicInfoModel
                {
                    Id = x.Id,
                    PersonalName = x.PersonalName,
                    Surname = x.Surname,
                })
                .ToList();

            return result;
        }

        public Author GetById(int id)
        {
            var author = this.authorRepository.All().Include(x => x.Books).ThenInclude(x => x.BookGenres).ThenInclude(x => x.Genre).Include(x => x.PseudonymFor).FirstOrDefault(x => x.Id == id);

            return author;
        }

        public async Task PersistEditedAuthorToDb()
        {
            await this.authorRepository.SaveChangesAsync();
        }
    }
}
