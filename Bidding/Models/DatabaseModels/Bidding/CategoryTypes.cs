using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class CategoryTypes
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }

        public Categories Category { get; set; }
        public Types Type { get; set; }
    }
}
