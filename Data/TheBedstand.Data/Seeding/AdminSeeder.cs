namespace TheBedstand.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
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
                UserName = configuration["Admin:Username"],
                Email = configuration["Admin:Email"]
            };

            var password = configuration["Admin:Password"];

            var result = await userManager.CreateAsync(admin, password);

            if (result.Succeeded)
            {
                await userManager.AddToRolesAsync(admin, new string[] { GlobalConstants.AdministratorRoleName, GlobalConstants.UserRoleName });
            }
        }
    }
}
