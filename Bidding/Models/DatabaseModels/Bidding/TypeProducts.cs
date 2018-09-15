using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class TypeProducts
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int ProductId { get; set; }

        public Products Product { get; set; }
        public Types Type { get; set; }
    }
}
