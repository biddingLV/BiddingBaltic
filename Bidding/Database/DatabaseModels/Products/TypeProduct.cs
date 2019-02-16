using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class TypeProduct
    {
        // todo: kke: improve this!
        public int TypeProductId { get; set; }
        public int TypeId { get; set; }
        public int ProductId { get; set; }

        public Product Product { get; set; }
        public Type Type { get; set; }
    }
}
