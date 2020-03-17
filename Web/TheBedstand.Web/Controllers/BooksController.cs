namespace TheBedstand.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using TheBedstand.Services.Data;

    public class BooksController : Controller
    {
        private readonly IBooksService booksService;

        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        public IActionResult All()
        {
            var model = this.booksService.All();

            return this.View(model);
        }
    }
}
