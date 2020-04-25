namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Common;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Administrators;
    using TheBedstand.Web.ViewModels.Administrators;

    public class AdministratorsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AdministratorsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await this.userManager.GetUsersInRoleAsync(GlobalConstants.AdministratorRoleName);

            var model = users.Where(x => x.UserName != "root_user").Select(x => new AdminListViewModel { Id = x.Id, Username = x.UserName });

            return this.View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AdministratorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var admin = new ApplicationUser { UserName = input.Username, Email = input.Email, };
            var result = await this.userManager.CreateAsync(admin, input.Password);

            if (result.Succeeded)
            {
                await this.userManager.AddToRoleAsync(admin, GlobalConstants.AdministratorRoleName);

                return this.RedirectToAction("All");
            }
            else
            {
                return this.View(input);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);

            var userRoles = await this.userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                await this.userManager.RemoveFromRoleAsync(user, role.ToString());
            }

            var result = await this.userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                this.TempData["result"] = "Success!";
                return this.RedirectToAction("All");
            }
            else
            {
                this.TempData["result"] = "Failure!";
                return this.RedirectToAction("All");
            }
        }
    }
}
