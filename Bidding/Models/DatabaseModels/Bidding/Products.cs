using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Products
    {
        public Products()
        {
            ProductDetails = new HashSet<ProductDetails>();
            TypeProducts = new HashSet<TypeProducts>();
        }

        public int Id { get; set; }

        public ICollection<ProductDetails> ProductDetails { get; set; }
        public ICollection<TypeProducts> TypeProducts { get; set; }
    }
}
