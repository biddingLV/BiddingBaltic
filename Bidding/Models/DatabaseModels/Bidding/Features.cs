using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Features
    {
        public Features()
        {
            ProductDetails = new HashSet<ProductDetails>();
        }

        public int Id { get; set; }

        public ICollection<ProductDetails> ProductDetails { get; set; }
    }
}
