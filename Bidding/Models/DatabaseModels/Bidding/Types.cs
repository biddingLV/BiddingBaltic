using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Types
    {
        public Types()
        {
            CategoryTypes = new HashSet<CategoryTypes>();
            TypeProducts = new HashSet<TypeProducts>();
        }

        public int Id { get; set; }

        public ICollection<CategoryTypes> CategoryTypes { get; set; }
        public ICollection<TypeProducts> TypeProducts { get; set; }
    }
}
