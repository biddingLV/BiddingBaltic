using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Feature
    {
        public Feature()
        {
            ProductDetails = new HashSet<ProductDetail>();
        }

        public int Id { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
