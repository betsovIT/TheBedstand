using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheBedstand.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class AdministratorController : AdministrationController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}