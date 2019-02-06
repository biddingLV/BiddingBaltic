using BiddingAPI.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Filters
{
    /// <summary>
    /// Used for top category filter in auction list
    /// </summary>
    public class TopCategoryFilterModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryTotalCount { get; set; }
    }
}
