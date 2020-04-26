namespace TheBedstand.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using Microsoft.EntityFrameworkCore;
    using TheBedstand.Data.Common.Repositories;
    using TheBedstand.Data.Models;
    using TheBedstand.Services;
    using TheBedstand.Web.InputModels.Books;
    using TheBedstand.Web.ViewModels.Books;
    using TheBedstand.Web.ViewModels.Comments;

    public class BooksService : IBooksService
    {
        private readonly IDeletableEntityRepository<Book> booksRepository;
        private readonly IDeletableEntityRepository<Comment> commentRepository;
        private readonly ICloudinaryService cloudinaryService;

        public BooksService(IDeletableEntityRepository<Book> booksRepository, IDeletableEntityRepository<Comment> commentRepository, ICloudinaryService cloudinaryService)
        {
            this.booksRepository = booksRepository;
            this.commentRepository = commentRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public IEnumerable<BookInfoViewModel> All()
        {
            var books = this.booksRepository
                 .All()
                 .Select(b => new BookInfoViewModel
                 {
                     Title = b.Title,
                     PageCount = b.PageCount,
                     CoverUrl = b.CoverUrl,
                     ShortAnnotation = b.Annotation != null ? b.Annotation.Substring(0, Math.Min(b.Annotation.Length, 100)) : null,
                     LongAnnotation = b.Annotation,
                     PublishedOn = b.PublishedOn,
                     Author = b.Author,
                     Id = b.Id,
                     Genres = b.BookGenres.Select(x => x.Genre.Name).ToList(),
                     Comments = this.commentRepository.All().Where(x => x.BookId == b.Id).Select(x => new CommentContentViewModel
                     {
                         Id = x.Id,
                         Content = x.Content,
                         CreatedOn = x.CreatedOn,
                         UserAvatarUrl = x.User.AvatarUrl,
                         Username = x.User.UserName,
                     }),
                 })
                 .ToList();

            return books;
        }

        public async Task Create(BookInputModel input, ImageUploadResult result)
        {
            var book = new Book
            {
                Title = input.Title,
                Id = input.ISBN,
                AuthorId = input.AuthorId,
                CoverId = result?.PublicId,
                CoverUrl = result?.Uri?.AbsoluteUri,
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

        public async Task<bool> DeleteBook(string id)
        {
            var book = this.booksRepository.All().FirstOrDefault(x => x.Id == id);
            var comments = this.commentRepository.All().Where(x => x.BookId == book.Id);

            if (book != null)
            {
                foreach (var comment in comments)
                {
                    this.commentRepository.HardDelete(comment);
                }

                this.booksRepository.HardDelete(book);
                await this.booksRepository.SaveChangesAsync();

                await this.cloudinaryService.Delete(book.CoverUrl);

                return true;
            }

            return false;
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
                     Comments = this.commentRepository.All().Where(x => x.BookId == b.Id).Select(x => new CommentContentViewModel
                     {
                         Id = x.Id,
                         Content = x.Content,
                         CreatedOn = x.CreatedOn,
                         UserAvatarUrl = x.User.AvatarUrl,
                         Username = x.User.UserName,
                     }),
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
                         Id = x.Id,
                         Content = x.Content,
                         CreatedOn = x.CreatedOn,
                         UserAvatarUrl = x.User.AvatarUrl,
                         Username = x.User.UserName,
                         UserId = x.User.Id,
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
