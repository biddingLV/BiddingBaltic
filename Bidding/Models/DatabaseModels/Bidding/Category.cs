using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Category
    {
        public Category()
        {
            CategoryTypes = new HashSet<CategoryType>();
        }

        public int Id { get; set; }

        public ICollection<CategoryType> CategoryTypes { get; set; }
    }
}
