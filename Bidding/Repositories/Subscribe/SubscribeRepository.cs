using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bidding.Models.DatabaseModels.Bidding.Subscribe;
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

        public bool UsingEmail(EmailRequestModel request)
        {
            bool categoryVehicles = (request.Categories.Contains("vehicles")) ? true : false;
            bool categoryItems = (request.Categories.Contains("items")) ? true : false;
            bool categoryCompanies = (request.Categories.Contains("companies")) ? true : false;
            bool categoryEstate = (request.Categories.Contains("estate")) ? true : false;
            bool categoryBrands = (request.Categories.Contains("brands")) ? true : false;

            Newsletter newsletter = new Newsletter()
            {
                Name = request.Name,
                Email = request.Email,
                Vehicles = categoryVehicles,
                Items = categoryItems,
                Companies = categoryCompanies,
                Estate = categoryEstate,
                Brands = categoryBrands,
                CreatedDate = DateTime.Now
            };

            m_context.Add(newsletter);
            return m_context.SaveChanges() == 1;
        }

        public bool UsingWhatsApp(WhatsAppRequestModel request)
        {
            bool categoryVehicles = (request.Categories.Contains("vehicles")) ? true : false;
            bool categoryItems = (request.Categories.Contains("items")) ? true : false;
            bool categoryCompanies = (request.Categories.Contains("companies")) ? true : false;
            bool categoryEstate = (request.Categories.Contains("estate")) ? true : false;
            bool categoryBrands = (request.Categories.Contains("brands")) ? true : false;

            Newsletter newsletter = new Newsletter()
            {
                Name = request.Name,
                Phone = request.Phone,
                Vehicles = categoryVehicles,
                Items = categoryItems,
                Companies = categoryCompanies,
                Estate = categoryEstate,
                Brands = categoryBrands,
                CreatedDate = DateTime.Now
            };

            m_context.Add(newsletter);
            return m_context.SaveChanges() == 1;
        }

        public bool UsingSurvey(SurveyRequestModel request)
        {
            return true;
        }
    }
}
