using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class ProductDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FeatureId { get; set; }

        public Features Feature { get; set; }
        public Products Product { get; set; }
    }
}
