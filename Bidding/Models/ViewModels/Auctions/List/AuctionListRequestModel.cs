using Bidding.Models.ViewModels.BaseModels;
using System.Collections.Generic;

namespace Bidding.Models.ViewModels.Auctions.List
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
