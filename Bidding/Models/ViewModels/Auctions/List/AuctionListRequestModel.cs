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

        /// <summary>
        /// Extra flag used to filter out auctions in list based on user role / permissions.
        /// For example, if user role AuctionCreator in admin panel auction list show only own auctions.
        /// For pageAdministrator show all auctions and so on.
        /// </summary>
        public string CalledFrom { get; set; }
    }
}
