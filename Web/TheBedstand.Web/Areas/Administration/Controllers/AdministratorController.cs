namespace TheBedstand.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class AdministratorController : AdministrationController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
