using Bidding.Models.ViewModels.Shared.Types;

namespace Bidding.Models.ViewModels.Filters
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
