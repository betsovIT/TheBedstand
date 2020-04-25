namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
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
        private readonly IBooksService booksService;

        public AuthorsService(IDeletableEntityRepository<Author> authorRepository, IBooksService booksService)
        {
            this.authorRepository = authorRepository;
            this.booksService = booksService;
        }

        public async Task CreateAsync(AuthorInputModel input, ImageUploadResult result)
        {
            var author = new Author
            {
                PersonalName = input.PersonalName,
                Surname = input.Surname,
                Biography = input.Biography,
                DateOfBirth = input.DateOfBirth,
                Country = input.Country,
                PseudonymForId = input.PseudonymForId,
                ImageUrl = result?.Uri?.AbsoluteUri,
                ImageId = result?.PublicId,
            };

            await this.authorRepository.AddAsync(author);
            await this.authorRepository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var author = this.authorRepository.All().Include(x => x.Books).FirstOrDefault(x => x.Id == id);

            foreach (var book in author.Books)
            {
                await this.booksService.DeleteBook(book.Id);
            }

            this.authorRepository.Delete(author);
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
