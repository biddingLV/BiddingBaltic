﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Services.Auctions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiddingAPI.Controllers.Auctions
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
        /// Gets Auction list and also used for to search for auction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Search([FromQuery] AuctionListRequestModel request)
        {
            // todo: kke: refactor filter ids to be list<int> in AuctionListRequestModel!
            return Ok(m_auctionsService.ListWithSearch(request));
        }

        /// <summary>
        /// Loads filters for auction list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        public IActionResult Details([FromQuery] AuctionDetailsRequestModel request)
        {
            return Ok(m_auctionsService.Details(request));
        }

        /// <summary>
        /// Adds a new auction
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] AuctionAddRequestModel request)
        {
            return Ok(m_auctionsService.Create(request));
        }

        [HttpPut]
        public IActionResult Edit([FromBody] AuctionEditRequestModel request)
        {
            return Ok(m_auctionsService.Update(request));
        }

        // toodo: kke: it can be from fromUrl for delete!
        [HttpDelete]
        public IActionResult Delete([FromBody] AuctionDeleteRequestModel request)
        {
            return Ok(m_auctionsService.Delete(request));
        }
    }
}
