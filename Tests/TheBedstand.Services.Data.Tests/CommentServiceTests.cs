namespace TheBedstand.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheBedstand.Data.Models;
    using TheBedstand.Data.Repositories;
    using TheBedstand.Data.Seeding;
    using TheBedstand.Services.Data.Tests.Common;
    using TheBedstand.Services.Data.Tests.Common.Seeders;
    using Xunit;

    public class CommentServiceTests
    {
        [Fact]
        public async Task CreateWithCorrectDataShouldReturnCorrectResult()
        {
            // Arrange
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var commentRepository = new EfDeletableEntityRepository<Comment>(context);
            var commentService = new CommentsService(commentRepository);

            var bookSeeder = new BookInMemorySeeder();
            var userSeeder = new UserSeeder();

            await bookSeeder.SeedBookAsync(context);
            await userSeeder.SeedUserAsync(context);
        }
    }
}
