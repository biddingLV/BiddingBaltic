using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bidding.Controllers.Shared
{
    public class PermissionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}