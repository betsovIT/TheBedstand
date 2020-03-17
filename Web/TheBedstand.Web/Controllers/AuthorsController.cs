namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TheBedstand.Common;
    using TheBedstand.Common.Helpers;
    using TheBedstand.Services;
    using TheBedstand.Services.Data;
    using TheBedstand.Web.InputModels.Authors;

    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly ICloudinaryService cloudinaryService;

        public AuthorsController(IAuthorsService authorsService, ICloudinaryService cloudinaryService)
        {
            this.authorsService = authorsService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authorsSelectList = this.authorsService.GetAll().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = NameHelper.GetFullName(x.PersonalName, x.Surname),
            });

            this.ViewData.Add("Authors", authorsSelectList);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var imageUrl = await this.cloudinaryService
                .UploadPhotoAsync(input.Image, $"{input.PersonalName + " " + input.Surname}", GlobalConstants.CloudFolderForAuthorsPhotos);

            await this.authorsService.CreateAsync(input, imageUrl);

            return this.RedirectToAction("Index", "Home");
        }
    }
}
