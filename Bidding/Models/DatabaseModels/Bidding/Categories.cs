using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Categories
    {
        public Categories()
        {
            CategoryTypes = new HashSet<CategoryTypes>();
        }

        public int Id { get; set; }

        public ICollection<CategoryTypes> CategoryTypes { get; set; }
    }
}
