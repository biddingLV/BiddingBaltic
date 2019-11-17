using Bidding.Services.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Bidding.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class FileUploaderController : ControllerBase
    {
        public readonly FileUploaderService m_fileUploaderService;

        public FileUploaderController(FileUploaderService fileUploaderService)
        {
            m_fileUploaderService = fileUploaderService ?? throw new ArgumentNullException(nameof(fileUploaderService));
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Upload()
        {
            IFormFileCollection files = Request.Form.Files;

            return Ok(await m_fileUploaderService.Upload(files));
        }
    }
}
