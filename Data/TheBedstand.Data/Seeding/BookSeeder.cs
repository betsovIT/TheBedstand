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
                Annotation = "In ancient times the Rings of Power were crafted by the Elven-smiths, and Sauron, the Dark Lord, forged the One Ring, filling it with his own power so that he could rule all others. But the One Ring was taken from him, and though he sought it throughout Middle-earth, it remained lost to him. After many ages it fell by chance into the hands of the hobbit Bilbo Baggins." + "\n" +
                "From Sauron's fastness in the Dark Tower of Mordor, his power spread far and wide. Sauron gathered all the Great Rings to him, but always he searched for the One Ring that would complete his dominion." + "\n" +
                "When Bilbo reached his eleventy-first birthday he disappeared, bequeathing to his young cousin Frodo the Ruling Ring and a perilous quest: to journey across Middle-earth, deep into the shadow of the Dark Lord, and destroy the Ring by casting it into the Cracks of Doom." +
                "The Lord of the Rings tells of the great quest undertaken by Frodo and the Fellowship of the Ring: Gandalf the Wizard; the hobbits Merry, Pippin, and Sam; Gimli the Dwarf; Legolas the Elf; Boromir of Gondor; and a tall, mysterious stranger called Strider",
                PageCount = 1216,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584092658/book_covers/8134AkhQJgL_ykzfyd.jpg",
                CoverId = "book_covers/8134AkhQJgL_ykzfyd",
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
                Annotation = @"After Englishman John Blackthorne is lost at sea, he awakens in a place few Europeans know of and even fewer have seen--Nippon. Thrust into the closed society that is seventeenth-century Japan, a land where the line between life and death is razor-thin, Blackthorne must negotiate not only a foreign people, with unknown customs and language, but also his own definitions of morality, truth, and freedom. As internal political strife and a clash of cultures lead to seemingly inevitable conflict, Blackthorne's loyalty and strength of character are tested by both passion and loss, and he is torn between two worlds that will each be forever changed." + "\n" +
                @"Powerful and engrossing, capturing both the rich pageantry and stark realities of life in feudal Japan, Shōgun is a critically acclaimed powerhouse of a book. Heart-stopping, edge-of-your-seat action melds seamlessly with intricate historical detail and raw human emotion. Endlessly compelling, this sweeping saga captivated the world to become not only one of the best-selling novels of all time but also one of the highest-rated television miniseries, as well as inspiring a nationwide surge of interest in the culture of Japan. Shakespearean in both scope and depth, Shōgun is, as the New York Times put it, ""...not only something you read--you live it."" Provocative, absorbing, and endlessly fascinating, there is only one: Shōgun.",
                PageCount = 1152,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584096198/book_covers/shogun-11_j9p8zs.jpg",
                CoverId = "book_covers/shogun-11_j9p8zs",
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
                Annotation = "For twelve thousand years the Galactic Empire has ruled supreme. Now it is dying. But only Hari Seldon, creator of the revolutionary science of psychohistory, can see into the future -- to a dark age of ignorance, barbarism, and warfare that will last thirty thousand years. To preserve knowledge and save mankind, Seldon gathers the best minds in the Empire -- both scientists and scholars -- and brings them to a bleak planet at the edge of the Galaxy to serve as a beacon of hope for a future generations. He calls his sanctuary the Foundation." + "\n" +
                "But soon the fledgling Foundation finds itself at the mercy of corrupt warlords rising in the wake of the receding Empire. Mankind's last best hope is faced with an agonizing choice: submit to the barbarians and be overrun -- or fight them and be destroyed.",
                PageCount = 255,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584096722/book_covers/Foundation_gnome_pddwbp.jpg",
                CoverId = "book_covers/Foundation_gnome_pddwbp",
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
                Annotation = "This is the way the world ends: with a nanosecond of computer error in a Defense Department laboratory and a million casual contacts that form the links in a chain letter of death. And here is the bleak new world of the day after: a world stripped of its institutions and emptied of 99 percent of its people. A world in which a handful of panicky survivors choose sides -- or are chosen.",
                PageCount = 823,
                CoverUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584096893/book_covers/The_Stand_cover_hhmcca.jpg",
                CoverId = "book_covers/The_Stand_cover_hhmcca",
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
