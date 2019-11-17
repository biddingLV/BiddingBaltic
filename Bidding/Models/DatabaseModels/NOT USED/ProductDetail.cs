using System;
using System.Collections.Generic;

namespace Bidding.Models.DatabaseModels
{
    public partial class ProductDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FeatureId { get; set; }

        public Feature Feature { get; set; }
        public Product Product { get; set; }
    }
}
