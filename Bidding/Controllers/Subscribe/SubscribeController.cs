﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;
using BiddingAPI.Services.Subscribe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bidding.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SubscribeController : Controller
    {
        public readonly ISubscribeService m_subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            m_subscribeService = subscribeService ?? throw new ArgumentNullException(nameof(subscribeService));
        }

        [HttpPut]
        [Route("public")]
        public IActionResult Public(int id)
        {
            return Json(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpPost]
        public async Task<IActionResult> UsingEmail([FromBody] EmailRequestModel request)
        {
            return Json(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
            // return Ok(await m_subscribeService.UsingEmail(request));
        }

        [HttpPut]
        public async Task<IActionResult> UsingWhatsApp([FromBody] WhatsAppRequestModel request)
        {
            return Ok(await m_subscribeService.UsingWhatsApp(request));
        }

        [HttpPut]
        public async Task<IActionResult> UsingSurvey([FromBody] SurveyRequestModel request)
        {
            return Ok(await m_subscribeService.UsingSurvey(request));
        }
    }
}
