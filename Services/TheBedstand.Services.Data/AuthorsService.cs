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

        public AuthorDetailsViewModel GetById(int id)
        {
            var author = this.authorRepository.All().Include(x => x.Books).ThenInclude(x => x.BookGenres).ThenInclude(x => x.Genre).FirstOrDefault(x => x.Id == id);

            var country = EnumDescriptionHelper.GetEnumValueDescription<Country>(author.Country);

            var result = new AuthorDetailsViewModel
            {
                PersonalName = author.PersonalName,
                Surname = author.Surname,
                DateOfBirth = author.DateOfBirth,
                Country = country,
                ImageUrl = author.ImageUrl,
                Biography = author.Biography,
                PseudonymForAuthor = author.PseudonymFor == null ? "N/A" : NameHelper.GetFullName(author.PseudonymFor.PersonalName, author.PseudonymFor.Surname),
                Genres = author.Books.SelectMany(b => b.BookGenres).Select(bg => bg.Genre?.Name).Distinct().ToArray(),
                Books = author.Books.Select(b => new BookAuthorPageViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    CoverUrl = b.CoverUrl,
                }).ToList(),
            };

            return result;
        }
    }
}
