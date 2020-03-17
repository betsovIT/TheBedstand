namespace TheBedstand.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TheBedstand.Data.Models;

    public class BookSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Books.Any())
            {
                return;
            }

            Book[] initialBooks = new Book[]
            {
            new Book
            {
                Id = "9780261103252",
                Title = "The Lord of the Rings",
                PublishedOn = new DateTime(1955, 10, 20),
                PageCount = 1216,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584092658/book_covers/8134AkhQJgL_ykzfyd.jpg",
                AuthorId = dbContext.Authors.First(x => x.PersonalName == "J. R. R." && x.Surname == "Tolkien").Id,
                BookGenres = new HashSet<BookGenre>
                {
                    new BookGenre
                    {
                        GenreId = dbContext.Genres.First(x => x.Name == "Classics").Id,
                    },
                    new BookGenre
                    {
                        GenreId = dbContext.Genres.First(x => x.Name == "Fantasy").Id,
                    },
                },
            },
            new Book
            {
                Id = "9780440178002",
                Title = "Shogun",
                PublishedOn = new DateTime(1975, 1, 1),
                PageCount = 1152,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584096198/book_covers/shogun-11_j9p8zs.jpg",
                AuthorId = dbContext.Authors.First(x => x.PersonalName == "James" && x.Surname == "Clavell").Id,
                BookGenres = new HashSet<BookGenre>
                {
                    new BookGenre
                    {
                        GenreId = dbContext.Genres.First(x => x.Name == "Classics").Id,
                    },
                    new BookGenre
                    {
                        BookId = "9780440178002",
                        GenreId = dbContext.Genres.First(x => x.Name == "History").Id,
                    },
                },
            },
            new Book
            {
                Id = "9780553293357",
                Title = "Foundation",
                PublishedOn = new DateTime(1951, 1, 1),
                PageCount = 255,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584096722/book_covers/Foundation_gnome_pddwbp.jpg",
                AuthorId = dbContext.Authors.First(x => x.PersonalName == "Isaac" && x.Surname == "Asimov").Id,
                BookGenres = new HashSet<BookGenre>
                {
                    new BookGenre
                    {
                        GenreId = dbContext.Genres.First(x => x.Name == "Classics").Id,
                    },
                    new BookGenre
                    {
                        GenreId = dbContext.Genres.First(x => x.Name == "Science Fiction").Id,
                    },
                },
            },
            new Book
            {
                Id = "9780385121682",
                Title = "The Stand",
                PublishedOn = new DateTime(1978, 10, 3),
                PageCount = 823,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584096893/book_covers/The_Stand_cover_hhmcca.jpg",
                AuthorId = dbContext.Authors.First(x => x.PersonalName == "Stephen" && x.Surname == "King").Id,
                BookGenres = new HashSet<BookGenre>
                {
                    new BookGenre
                    {
                        GenreId = dbContext.Genres.First(x => x.Name == "Mistery").Id,
                    },
                },
            },
            };

            foreach (var book in initialBooks)
            {
                await dbContext.Books.AddAsync(book);
            }
        }
    }
}
