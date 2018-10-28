using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Product
    {
        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
            TypeProducts = new HashSet<TypeProduct>();
        }

        public int Id { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
        public ICollection<TypeProduct> TypeProducts { get; set; }
    }
}
