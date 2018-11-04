using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class CategoryType
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }

        public Category Category { get; set; }
        public Type Type { get; set; }
    }
}
