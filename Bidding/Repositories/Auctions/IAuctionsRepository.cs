using System;
using System.Collections.Generic;
using Bidding.Models.ViewModels.Bidding.Auctions.Details;
using Bidding.Models.ViewModels.Bidding.Auctions.List;
using Bidding.Models.ViewModels.Bidding.Filters;
using BiddingAPI.Models.DatabaseModels;
using BiddingAPI.Models.DatabaseModels.Bidding;
using BiddingAPI.Models.ViewModels.Bidding.Auctions;

namespace BiddingAPI.Repositories.Auctions
{
    public interface IAuctionsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="startFrom">pagination start from</param>
        /// <param name="endAt"> pagination end at</param>
        /// <param name="categoryIds">top category ids</param>
        /// <param name="typeIds">sub-category ids</param>
        /// <returns></returns>
        IEnumerable<AuctionListModel> ListWithSearch(AuctionListRequestModel request, int startFrom, int endAt, List<int> categoryIds, List<int> typeIds);

        /// <summary>
        /// Gets total auction count based on specific date/time range for pagination
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns></returns>
        IEnumerable<Auction> TotalAuctionCount(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Loads top categories with total count
        /// </summary>
        /// <returns></returns>
        IEnumerable<TopCategoryFilterModel> LoadTopCategories();

        /// <summary>
        /// Loads sub-categories with total count
        /// </summary>
        /// <returns></returns>
        IEnumerable<SubCategoryFilterModel> LoadSubCategories();

        IEnumerable<AuctionDetailsResponseModel> Details(AuctionDetailsRequestModel request);

        bool Update(AuctionEditRequestModel request);

        bool Create(AuctionAddRequestModel request);

        bool Delete(AuctionDeleteRequestModel request);
    }
}
