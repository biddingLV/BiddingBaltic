using Bidding.Models.ViewModels.Subscribe;
using Bidding.Services.Subscribe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bidding.Controllers.Subscribe
{
    [Produces("application/json")]
    [Route("api/[Controller]/[action]")]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscribeService m_subscribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            m_subscribeService = subscribeService ?? throw new ArgumentNullException(nameof(subscribeService));
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult UsingEmail([FromBody] EmailRequestModel request)
        {
            return Ok(m_subscribeService.UsingEmail(request));
        }

        [AllowAnonymous]
        [HttpPut]
        public IActionResult UsingWhatsApp([FromBody] WhatsAppRequestModel request)
        {
            return Ok(m_subscribeService.UsingWhatsApp(request));
        }
    }
}
