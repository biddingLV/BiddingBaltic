using Bidding.Models.DatabaseModels;
using Bidding.Models.ViewModels.Bidding.Shared.Types;
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
    public class SubCategoryFilterModel : TypeModel
    {
        public int TypeTotalCount { get; set; }
    }
}
