﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Auctions;
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
    public class AuctionsController : Controller
    {
        public readonly IAuctionsService m_auctionsService;

        public AuctionsController(IAuctionsService auctionsService)
        {
            m_auctionsService = auctionsService ?? throw new ArgumentNullException(nameof(auctionsService));
        }

        [HttpGet]
        public IActionResult Search([FromQuery] AuctionListRequestModel request)
        {
            return Ok(m_auctionsService.Search(request));
        }

        [HttpGet]
        public IActionResult Details([FromQuery] int auctionId)
        {
            return Ok(m_auctionsService.Details(auctionId));
        }

        // filter
        [HttpGet]
        public IActionResult Categories()
        {
            return Ok(m_auctionsService.Categories());
        }

        // add
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

        [HttpDelete]
        public IActionResult Delete([FromBody] AuctionDeleteRequestModel request)
        {
            return Ok(m_auctionsService.Delete(request));
        }
    }
}
