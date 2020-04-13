using Bidding.Models.ViewModels.Auctions.Add;
using Bidding.Models.ViewModels.Auctions.Delete;
using Bidding.Models.ViewModels.Auctions.Details;
using Bidding.Models.ViewModels.Auctions.Edit;
using Bidding.Models.ViewModels.Auctions.List;
using Bidding.Services.Auctions;
using FeatureAuthorize.PolicyCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionParts;
using System;
using System.Threading.Tasks;

namespace Bidding.Controllers.Auctions
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class AuctionsController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public AuctionsController(IAuctionService auctionService)
        {
            _auctionService = auctionService ?? throw new ArgumentNullException(nameof(auctionService));
        }

        /// <summary>
        /// Fetch only Active auctions, atm used in homepage and auction list page.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetActiveAuctions([FromQuery] AuctionListRequestModel request)
        {
            return Ok(_auctionService.GetActiveAuctions(request));
        }

        /// <summary>
        /// Loads filters for auction list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Filters()
        {
            return Ok(_auctionService.Filters());
        }

        /// <summary>
        /// Gets specific auction details
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details([FromQuery] AuctionDetailsRequestModel request)
        {
            return Ok(await _auctionService.DetailsAsync(request).ConfigureAwait(true));
        }

        /// <summary>
        /// Gets all auctions, active, not active for admin page.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.AccessAdminPanel)]
        public IActionResult GetAllAuctions([FromQuery] AuctionListRequestModel request)
        {
            return Ok(_auctionService.GetAllAuctions(request));
        }

        /// <summary>
        /// Fetch all top-categories with sub-categories / types for auction add wizard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult CategoriesWithTypes()
        {
            return Ok(_auctionService.CategoriesWithTypes());
        }

        /// <summary>
        /// Fetch auction format list for auction add modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult Formats()
        {
            return Ok(_auctionService.Formats());
        }

        /// <summary>
        /// Fetch auction status list for auction add modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult Statuses()
        {
            return Ok(_auctionService.Statuses());
        }

        /// <summary>
        /// Creates auction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult Create([FromBody] AddAuctionRequestModel request)
        {
            return Ok(_auctionService.Create(request));
        }

        /// <summary>
        /// Loads specific auction's details for update modal
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.CreateAuction)] // TODO: KKE: Add correct permission!
        public IActionResult EditDetails([FromQuery] int auctionId)
        {
            return Ok(_auctionService.EditDetails(auctionId));
        }

        /// <summary>
        /// Updates 1 auction in go
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [HasPermission(Permission.CreateAuction)] // TODO: KKE: Add correct permission!
        public IActionResult Edit([FromBody] AuctionEditRequestModel request)
        {
            return Ok(_auctionService.UpdateAuctionDetails(request));
        }

        /// <summary>
        /// Can be deleted 1+ auctions, deleted means column "Deleted" to be set true(soft delete)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [HasPermission(Permission.CreateAuction)] // TODO: KKE: Add correct permission!
        public async Task<IActionResult> Delete([FromBody] AuctionDeleteRequestModel request)
        {
            return Ok(await _auctionService.DeleteAsync(request).ConfigureAwait(true));
        }
    }
}
