using Bidding.Models.ViewModels.Bidding.Shared.Categories;
using Bidding.Models.ViewModels.Bidding.Shared.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bidding.Models.ViewModels.Bidding.Auctions.Add
{
    public class CategoriesWithTypesModel
    {
        public List<CategoryModel> TopCategories { get; set; }
        public List<TypeModel> SubCategories { get; set; }
    }
}
