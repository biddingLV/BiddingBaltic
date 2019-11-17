using Bidding.Models.ViewModels.Shared.Categories;
using Bidding.Models.ViewModels.Shared.Types;
using System.Collections.Generic;

namespace Bidding.Models.ViewModels.Auctions.Add
{
    public class CategoriesWithTypesModel
    {
        public List<CategoryModel> TopCategories { get; set; }
        public List<TypeModel> SubCategories { get; set; }
    }
}
