namespace TheBedstand.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using TheBedstand.Data;
    using TheBedstand.Data.Models;

    public class UserSeeder
    {
        public async Task SeedUserAsync(ApplicationDbContext context)
        {
            var user = new ApplicationUser
            {
                AvatarId = "someId",
                AvatarUrl = "someUrl",
                Email = "user@user.com",
                UserName = "user",
            };

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }
    }
}
