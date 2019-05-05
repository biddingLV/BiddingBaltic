using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Bidding.Models.DatabaseModels.Bidding.Subscribe;
using Bidding.Models.ViewModels.Bidding.Subscribe;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
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

        public bool UsingEmail(EmailRequestModel request)
        {
            bool categoryVehicles = request.Categories.Contains("vehicles") ? true : false;
            bool categoryItems = request.Categories.Contains("items") ? true : false;
            bool categoryEstate = request.Categories.Contains("estate") ? true : false;

            Newsletter newsletter = new Newsletter()
            {
                Name = request.Name,
                Email = request.Email,
                Vehicles = categoryVehicles,
                Items = categoryItems,
                Estate = categoryEstate
            };

            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        m_context.Add(newsletter);
                        m_context.SaveChanges();

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.SubscribeEmailFails, ex);
                }
            });

            return true;
        }

        public bool UsingWhatsApp(WhatsAppRequestModel request)
        {
            bool categoryVehicles = request.Categories.Contains("vehicles") ? true : false;
            bool categoryItems = request.Categories.Contains("items") ? true : false;
            bool categoryEstate = request.Categories.Contains("estate") ? true : false;

            Newsletter newsletter = new Newsletter()
            {
                Name = request.Name,
                Phone = request.Phone,
                Vehicles = categoryVehicles,
                Items = categoryItems,
                Estate = categoryEstate
            };

            var strategy = m_context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                try
                {
                    using (var transaction = m_context.Database.BeginTransaction())
                    {
                        m_context.Add(newsletter);
                        m_context.SaveChanges();

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessages.SubscribWhatsAppFails, ex);
                }
            });

            return true;
        }
    }
}
