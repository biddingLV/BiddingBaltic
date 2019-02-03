using BiddingAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Filters
{
    public class AuctionFilterModel
    {
        public List<Category> TopCategories { get; set; }
        public int TopCategoryCount { get; set; }
    }
}
