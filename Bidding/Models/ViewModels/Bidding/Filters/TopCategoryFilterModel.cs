using Bidding.Models.DatabaseModels;
using Bidding.Models.ViewModels.Bidding.Shared.Categories;
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
    public class TopCategoryFilterModel : CategoryModel
    {
        public int CategoryTotalCount { get; set; }
    }
}
