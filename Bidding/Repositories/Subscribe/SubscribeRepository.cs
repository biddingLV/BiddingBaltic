using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.ViewModels.Bidding.Subscribe;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Repositories.Subscribe;
using Microsoft.EntityFrameworkCore;

namespace BiddingAPI.Repositories.Subscribe
{
    public class SubscribeRepository : ISubscribeRepository
    {
        private readonly BiddingContext m_context;

        public SubscribeRepository(BiddingContext context)
        {
            m_context = context;
        }

        public async Task<bool> UsingEmail(EmailRequestModel request)
        {
            return true;
        }

        public async Task<bool> UsingWhatsApp(WhatsAppRequestModel request)
        {
            return true;
        }

        public async Task<bool> UsingSurvey(SurveyRequestModel request)
        {
            return true;
        }
    }
}
