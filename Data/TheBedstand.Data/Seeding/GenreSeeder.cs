namespace TheBedstand.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using TheBedstand.Common;
    using TheBedstand.Data.Models;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Genres;

    public class GenreSeeder : ISeeder
    {
        private readonly Genre[] initialGenres = new Genre[]
            {
                new Genre
                {
                    Name = "Classics",
                    Description = "Pieces of literature that have timeless quality or were the first of their genre.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584025739/genre_photos/Troy_vbigrw.jpg",
                },

                new Genre
                {
                    Name = "Science Fiction",
                    Description = "Science fiction is a genre that typically deals with imaginative and futuristic concepts such as advanced science and technology, space exploration, time travel, parallel universes, and extraterrestrial life.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584025855/genre_photos/foundation-asimov-seldon-1200x640_bhrfuu.jpg",
                },

                new Genre
                {
                    Name = "Fantasy",
                    Description = "Fantasy is a genre of speculative fiction set in a fictional universe, often inspired by real world myth and folklore. Its roots are in oral traditions, which then became fantasy literature and drama.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584027014/genre_photos/Morgoth_Fingolfin_m0tjn4.jpg",
                },

                new Genre
                {
                    Name = "Biography",
                    Description = "Biography is a literary genre that portrays the experiences of all these events occurring in the life of a person, mostly in a chronological order.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584026838/genre_photos/9780190262716_o1kzl8.jpg",
                },

                new Genre
                {
                    Name = "Mistery",
                    Description = "Mystery is a genre of literature whose stories focus on a puzzling crime, situation, or circumstance that needs to be solved.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584028264/genre_photos/12087039_1010378148984719_4724957302230297183_o_og3uye.jpg",
                },

                new Genre
                {
                    Name = "Science",
                    Description = "Books regarding a specific scientific consept, problem or an entire scientific field including ones aimed at a more generalized public.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584028454/genre_photos/March_for_Science_tev2ys.png",
                },

                new Genre
                {
                    Name = "Academic",
                    Description = "Text books for teaching purposes or scientific papers for circulation between the academic community.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584028822/genre_photos/PPCJ5POEBNBU3E7JR56RJ26NDY_il5nia.jpg",
                },

                new Genre
                {
                    Name = "Philosophy",
                    Description = "Books in the philosophy genre are about the fundamental nature of knowledge, reality, and existence as an academic discipline.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584028918/genre_photos/school_of_athens1348180322150_asuce0.jpg",
                },

                new Genre
                {
                    Name = "Religion",
                    Description = "Books in the religion n genre are about the organization of collective beliefs, culture, world views, meaning of life, the origins of life, and the universe as a whole, usually under a god or deity",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584029103/genre_photos/RELIGIONES_yizp7i.png",
                },

                new Genre
                {
                    Name = "Romance",
                    Description = "Two basic elements comprise every romance novel: a central love story and an emotionally satisfying and optimistic ending.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584029201/genre_photos/Wm._Blair_Leighton_s_The_End_of_Song_cb4adv.jpg",
                },

                new Genre
                {
                    Name = "Crime",
                    Description = "The crime genre includes the broad selection of books on criminals and the court system, but the most common focus is investigations and sleuthing.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584029280/genre_photos/jack-the-ripper-2_wfru1e.jpg",
                },

                new Genre
                {
                    Name = "History",
                    Description = "The history genre consists of books about historical events, persons and the consquences of them.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584029411/genre_photos/fall-of-carthage_fhngp9.jpg",
                },

                new Genre
                {
                    Name = "Children's",
                    Description = "Books aimed at children or people in early adolescense.",
                    ImageUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1584029469/genre_photos/41DfYlAxn6L._SX355_BO1_204_203_200__ixmjue.jpg",
                },
            };

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Genres.Any())
            {
                return;
            }

            foreach (var genre in this.initialGenres)
            {
               await dbContext.Genres.AddAsync(genre);
            }
        }
    }
}
