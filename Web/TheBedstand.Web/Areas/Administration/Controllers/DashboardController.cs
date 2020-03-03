namespace TheBedstand.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.ViewModels.Administration.Dashboard;

    [Area("Administration")]
    public class DashboardController : AdministrationController
    {
        private readonly ISettingsService settingsService;

        public DashboardController(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel { SettingsCount = this.settingsService.GetCount(), };
            return this.View(viewModel);
        }
    }
}
