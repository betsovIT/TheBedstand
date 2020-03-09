namespace TheBedstand.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using TheBedstand.Common;
    using TheBedstand.Services.Data;

    public class GenreSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var genreService = serviceProvider.GetRequiredService<IGenresService>();

            if (dbContext.Genres.Any())
            {
                return;
            }

            foreach (var genre in InitialSeedingEntities.InitialGenres)
            {
                await genreService.CreateAsync(genre);
            }
        }
    }
}
