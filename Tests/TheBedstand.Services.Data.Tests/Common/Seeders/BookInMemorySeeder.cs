namespace TheBedstand.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheBedstand.Data;
    using TheBedstand.Data.Models;

    public class BookInMemorySeeder
    {
        public async Task SeedBookAsync(ApplicationDbContext context)
        {
            var book = new Book
            {
                Title = "Book Title",
                Annotation = "Annotation",
                CoverId = "someId",
                CoverUrl = "someUrl",
                Id = "9788467225471",
                PageCount = 500,
                PublishedOn = DateTime.UtcNow,
                Author = new Author
                {
                    PersonalName = "Az",
                    Surname = "Azzzzzz",
                    Biography = "Biography",
                    Country = TheBedstand.Data.Models.Enums.Country.US,
                    ImageId = "someId",
                    ImageUrl = "someUrl",
                    DateOfBirth = DateTime.UtcNow,
                },
            };

            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
        }
    }
}
