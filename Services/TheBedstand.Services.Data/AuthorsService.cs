namespace TheBedstand.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
    using TheBedstand.Data.Models.Enums;
    using TheBedstand.Web.InputModels.Authors;

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

        public IEnumerable<Author> GetAll()
        {
            var result = this.authorRepository.AllAsNoTracking().ToList();

            return result;
        }
    }
}
