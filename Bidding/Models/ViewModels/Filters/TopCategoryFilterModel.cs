using Bidding.Models.ViewModels.Shared.Categories;

namespace Bidding.Models.ViewModels.Filters
{
    /// <summary>
    /// Used for top category filter in auction list
    /// </summary>
    public class TopCategoryFilterModel : CategoryModel
    {
        public int CategoryTotalCount { get; set; }
    }
}
