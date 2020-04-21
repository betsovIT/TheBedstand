namespace TheBedstand.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : Controller
    {
        public IActionResult Details(string userId)
        {
            return this.View();
        }
    }
}
