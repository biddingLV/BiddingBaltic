using Bidding.Models.Contexts;
using Bidding.Models.DatabaseModels.Subscribe;
using Bidding.Models.ViewModels.Subscribe;
using Bidding.Shared.ErrorHandling.Errors;
using Bidding.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace Bidding.Repositories.Subscribe
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
                    throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.SubscribeEmailFails, ex);
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
                    throw new WebApiException(HttpStatusCode.BadRequest, UserErrorMessage.SubscribWhatsAppFails, ex);
                }
            });

            return true;
        }
    }
}
