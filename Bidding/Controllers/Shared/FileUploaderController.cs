using Bidding.Services.Shared;
using FeatureAuthorize.PolicyCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionParts;
using System;
using System.Threading.Tasks;

namespace Bidding.Controllers.Shared
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class FileUploaderController : ControllerBase
    {
        private readonly FileUploaderService m_fileUploaderService;

        public FileUploaderController(FileUploaderService fileUploaderService)
        {
            m_fileUploaderService = fileUploaderService ?? throw new ArgumentNullException(nameof(fileUploaderService));
        }

        [HttpPost]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult ValidateFiles()
        {
            IFormFileCollection files = Request.Form.Files;

            return Ok(m_fileUploaderService.ValidateFiles(files));
        }

        [HttpPost]
        [HasPermission(Permission.CreateAuction)]
        public async Task<IActionResult> UploadFiles()
        {
            IFormFileCollection files = Request.Form.Files;

            return Ok(await m_fileUploaderService.UploadFilesAsync(files).ConfigureAwait(true));
        }
    }
}
