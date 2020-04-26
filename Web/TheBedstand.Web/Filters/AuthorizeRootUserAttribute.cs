namespace TheBedstand.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using TheBedstand.Data.Models;

    public class AuthorizeRootUserAttribute : Attribute, IAsyncActionFilter
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;

        public AuthorizeRootUserAttribute(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            var user = await this.userManager.FindByIdAsync(userId.Value);

            if (user.UserName != this.configuration["Root:UserName"])
            {
                context.Result = new ForbidResult();
            }
            else
            {
                await next();
            }
        }
    }
}
