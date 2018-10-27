﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;
using BiddingAPI.Services.Auctions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BiddingAPI.Controllers.Auctions
{
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
            //if (User.Identity.IsAuthenticated)
            //{
            //    string accessToken = await HttpContext.GetTokenAsync("access_token");
            //    string idToken = await HttpContext.GetTokenAsync("id_token");

            //    // Now you can use them. For more info on when and how to use the 
            //    // access_token and id_token, see https://auth0.com/docs/tokens
            //}
            return Ok(m_auctionsService.Search(request));
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

        // testing
        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            return Json(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpGet]
        [Route("private")]
        [Authorize]
        public IActionResult Private()
        {
            return Json(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated to see this."
            });
        }
    }
}
