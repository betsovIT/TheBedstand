namespace TheBedstand.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using TheBedstand.Common;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Genres;

    public class GenreSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var genreService = serviceProvider.GetRequiredService<IGenresService>();

            if (dbContext.Genres.Any())
            {
                return;
            }

            var initialGenres = new CreateGenreInputModel[]
            {
                new CreateGenreInputModel
                {
                    Name = "Classics",
                    Description = "Pieces of literature that have timeless quality or were the first of their genre.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Science Fiction",
                    Description = "Science fiction is a genre that typically deals with imaginative and futuristic concepts such as advanced science and technology, space exploration, time travel, parallel universes, and extraterrestrial life.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Fantasy",
                    Description = "Fantasy is a genre of speculative fiction set in a fictional universe, often inspired by real world myth and folklore. Its roots are in oral traditions, which then became fantasy literature and drama.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Biography",
                    Description = "Biography is a literary genre that portrays the experiences of all these events occurring in the life of a person, mostly in a chronological order.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Mistery",
                    Description = "Mystery is a genre of literature whose stories focus on a puzzling crime, situation, or circumstance that needs to be solved.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Science",
                    Description = "Books regarding a specific scientific consept, problem or an entire scientific field including ones aimed at a more generalized public.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Academic",
                    Description = "Text books for teaching purposes or scientific papers for circulation between the academic community.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Philosophy",
                    Description = "Books in the philosophy genre are about the fundamental nature of knowledge, reality, and existence as an academic discipline.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Religion",
                    Description = "Books in the religion n genre are about the organization of collective beliefs, culture, world views, meaning of life, the origins of life, and the universe as a whole, usually under a god or deity",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Romance",
                    Description = "Two basic elements comprise every romance novel: a central love story and an emotionally satisfying and optimistic ending.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Crime",
                    Description = "The crime genre includes the broad selection of books on criminals and the court system, but the most common focus is investigations and sleuthing.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "History",
                    Description = "The history genre consists of books about historical events, persons and the consquences of them.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
                new CreateGenreInputModel
                {
                    Name = "Children's",
                    Description = "Books aimed at children or people in early adolescense.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1583845557/sample.jpg",
                },
            };

            foreach (var genre in initialGenres)
            {
                await genreService.CreateAsync(genre);
            }
        }
    }
}
