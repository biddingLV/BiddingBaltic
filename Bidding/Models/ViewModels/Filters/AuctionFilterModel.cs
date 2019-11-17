using Bidding.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Filters
{
    public class AuctionFilterModel
    {
        public List<TopCategoryFilterModel> TopCategories { get; set; }
        public List<SubCategoryFilterModel> SubCategories { get; set; }
    }
}
