namespace TheBedstand.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using TheBedstand.Common;
    using TheBedstand.Data.Models;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any())
            {
                return;
            }

            var admin = new ApplicationUser()
            {
                UserName = configuration["Root:Username"],
                Email = configuration["Root:Email"],
                AvatarId = "user_photos/Name-tag-admin-1000_bqr96j",
                AvatarUrl = "https://res.cloudinary.com/dzpsrlawz/image/upload/v1587712875/user_photos/Name-tag-admin-1000_bqr96j.webp",
            };

            var password = configuration["Root:Password"];

            var result = await userManager.CreateAsync(admin, password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(admin, new string[] { GlobalConstants.AdministratorRoleName, GlobalConstants.UserRoleName });
            }
        }
    }
}
