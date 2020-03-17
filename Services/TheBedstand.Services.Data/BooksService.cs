namespace TheBedstand.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
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
                     PublishedOn = b.PublishedOn,
                     Author = b.Author,
                     Id = b.Id,
                     Genres = b.BookGenres.Select(x => x.Genre.Name).ToList(),
                 }).ToList();

            return result;
        }
    }
}
