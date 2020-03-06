using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheBedstand.Web.Areas.Administration.Controllers
{
    public class GenresController : AdministrationController
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}