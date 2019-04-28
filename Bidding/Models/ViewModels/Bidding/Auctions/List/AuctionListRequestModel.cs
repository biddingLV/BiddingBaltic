using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiddingAPI.Models.ViewModels.BaseModels;

namespace BiddingAPI.Models.ViewModels.Bidding.Auctions
{
    public class AuctionListRequestModel : BaseListRequestModel
    {
        /// <summary>
        /// Top category filter ids
        /// </summary>
        public List<int> TopCategoryIds { get; set; }
        /// <summary>
        /// Type filter / sub-category filter ids
        /// </summary>
        public List<int> TypeIds { get; set; }
    }
}
