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
        private readonly AuctionsService m_auctionsService;

        public AuctionsController(AuctionsService auctionsService)
        {
            m_auctionsService = auctionsService ?? throw new ArgumentNullException(nameof(auctionsService));
        }

        /// <summary>
        /// Fetch auction list with filters, but without search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAuctions([FromQuery] AuctionListRequestModel request)
        {
            return Ok(m_auctionsService.GetAuctions(request));
        }

        /// <summary>
        /// Loads filters for auction list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Filters()
        {
            return Ok(m_auctionsService.Filters());
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
            return Ok(await m_auctionsService.DetailsAsync(request).ConfigureAwait(false));
        }

        /// <summary>
        /// Fetch auction list with filters, including search
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAuctionsWithSearch([FromQuery] AuctionListRequestModel request)
        {
            return Ok(m_auctionsService.GetAuctionsWithSearch(request));
        }

        /// <summary>
        /// Fetch all top-categories with sub-categories / types for auction add wizard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult CategoriesWithTypes()
        {
            return Ok(m_auctionsService.CategoriesWithTypes());
        }

        /// <summary>
        /// Fetch auction format list for auction add modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult Formats()
        {
            return Ok(m_auctionsService.Formats());
        }

        /// <summary>
        /// Fetch auction status list for auction add modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HasPermission(Permission.CreateAuction)]
        public IActionResult Statuses()
        {
            return Ok(m_auctionsService.Statuses());
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
            return Ok(m_auctionsService.Create(request));
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
            return Ok(m_auctionsService.EditDetails(auctionId));
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
            return Ok(m_auctionsService.UpdateAuctionDetails(request));
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
            return Ok(await m_auctionsService.DeleteAsync(request));
        }
    }
}
