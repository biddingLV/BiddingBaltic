using BiddingAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Filters
{
    /// <summary>
    /// Used for sub-category filter in auction list
    /// type == sub-category
    /// </summary>
    public class SubCategoryFilterModel
    {
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int TypeTotalCount { get; set; }
    }
}
