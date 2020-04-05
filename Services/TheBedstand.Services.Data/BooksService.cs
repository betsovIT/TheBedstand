namespace TheBedstand.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Books;
    using TheBedstand.Web.ViewModels.Books;

    public class BooksService : IBooksService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;

        public BooksService(IDeletableEntityRepository<Book> booksRepository)
        {
            this.booksRepository = booksRepository;
        }

        public IEnumerable<BookInfoViewModel> All()
        {
            var result = this.booksRepository
                 .All()
                 .Select(b => new BookInfoViewModel
                 {
                     Title = b.Title,
                     CoverUrl = b.CoverUrl,
                     PageCount = b.PageCount,
                     ShortAnnotation = b.Annotation != null ? b.Annotation.Substring(0, Math.Min(b.Annotation.Length, 100)) : null,
                     LongAnnotation = b.Annotation,
                     PublishedOn = b.PublishedOn,
                     Author = b.Author,
                     Id = b.Id,
                     Genres = b.BookGenres.Select(x => x.Genre.Name).ToList(),
                 }).ToList();

            return result;
        }

        public async Task Create(BookInputModel input, string imageUrl)
        {
            var book = new Book
            {
                Title = input.Title,
                Id = input.ISBN,
                AuthorId = input.AuthorId,
                CoverUrl = imageUrl,
                PageCount = input.PageCount,
                Annotation = input.Annotation,
            };

            foreach (var genreId in input.GenreIds)
            {
                book.BookGenres.Add(new BookGenre { GenreId = genreId, BookId = input.ISBN, });
            }

            await this.booksRepository.AddAsync(book);
            await this.booksRepository.SaveChangesAsync();
        }
    }
}
