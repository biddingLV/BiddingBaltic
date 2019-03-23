using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bidding.Controllers.Shared
{
    public class PermissionsController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}