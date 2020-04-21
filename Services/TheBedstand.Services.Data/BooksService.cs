namespace TheBedstand.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Books;
    using TheBedstand.Web.ViewModels.Books;
    using TheBedstand.Web.ViewModels.Comments;

    public class BooksService : IBooksService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Comment> commentRepository;

        public BooksService(IDeletableEntityRepository<Book> booksRepository, IDeletableEntityRepository<Comment> commentRepository)
        {
            this.booksRepository = booksRepository;
            this.commentRepository = commentRepository;
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

        public IEnumerable<BookInfoViewModel> GetByGenre(int id)
        {
            var result = this.booksRepository
                 .All()
                 .Where(x => x.BookGenres.Any(g => g.GenreId == id))
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

        public BookInfoViewModel GetById(string id)
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
                     Comments = this.commentRepository.All().Where(x => x.BookId == b.Id).Select(x => new CommentContentViewModel
                     {
                         Content = x.Content,
                         CreatedOn = x.CreatedOn,
                         UserAvatarId = x.User.AvatarId,
                         Username = x.User.UserName,
                     }),
                 })
                 .FirstOrDefault(x => x.Id == id);

            return result;
        }

        public Book GetByIdAsDbModel(string id)
        {
            var result = this.booksRepository
                 .All()
                 .Include(x => x.Author)
                 .FirstOrDefault(x => x.Id == id);

            return result;
        }

        public async Task PersistBookToDb(Book book, int[] genreIds)
        {
            if (genreIds != null)
            {
                var bookGenres = new HashSet<BookGenre>();

                foreach (int genreId in genreIds)
                {
                    var bookGenre = new BookGenre { BookId = book.Id, GenreId = genreId };

                    bookGenres.Add(bookGenre);
                }

                book.BookGenres = bookGenres;
            }

            await this.booksRepository.SaveChangesAsync();
        }
    }
}
