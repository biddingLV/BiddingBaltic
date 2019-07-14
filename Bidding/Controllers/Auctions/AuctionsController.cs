﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions.Add;
using Bidding.Models.ViewModels.Bidding.Auctions.Delete;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Auctions.Edit;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Services.Auctions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bidding.Controllers.Auctions
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class AuctionsController : ControllerBase
    {
        public readonly AuctionsService m_auctionsService;

        public AuctionsController(AuctionsService auctionsService)
        {
            m_auctionsService = auctionsService ?? throw new ArgumentNullException(nameof(auctionsService));
        }

        /// <summary>
        /// Gets Auction list and also used for to search for specific auction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Search([FromQuery] AuctionListRequestModel request)
        {
            return Ok(m_auctionsService.ListWithSearch(request));
        }

        /// <summary>
        /// Loads filters for auction list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Filters()
        {
            return Ok(m_auctionsService.Filters());
        }

        /// <summary>
        /// Fetch all top-categories with sub-categories / types for auction add wizard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult CategoriesWithTypes()
        {
            return Ok(m_auctionsService.CategoriesWithTypes());
        }

        /// <summary>
        /// Fetch auction creator list for auction add modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Creators()
        {
            return Ok(m_auctionsService.Creators());
        }

        /// <summary>
        /// Fetch auction format list for auction add modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Formats()
        {
            return Ok(m_auctionsService.Formats());
        }

        /// <summary>
        /// Fetch extra details for create vehicle auction 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateVehicleDetails()
        {
            return Ok(m_auctionsService.CreateVehicleDetails());
        }

        /// <summary>
        /// Fetch auction status list for auction add modal
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Statuses()
        {
            return Ok(m_auctionsService.Statuses());
        }

        /// <summary>
        /// Gets specific auction details
        /// </summary>
        /// <param name="auctionId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public IActionResult Details([FromQuery] AuctionDetailsRequestModel request)
        {
            return Ok(m_auctionsService.Details(request));
        }

        /// <summary>
        /// Creates auction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] AddAuctionRequestModel request)
        {
            return Ok(m_auctionsService.Create(request));
        }

        /// <summary>
        /// Updates 1 auction in go
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit([FromBody] AuctionEditRequestModel request)
        {
            return Ok(m_auctionsService.Update(request));
        }

        /// <summary>
        /// Can be deleted 1+ auctions, deleted means column "Deleted" to be set true(soft delete)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete([FromBody] AuctionDeleteRequestModel request)
        {
            return Ok(m_auctionsService.Delete(request));
        }
    }
}
