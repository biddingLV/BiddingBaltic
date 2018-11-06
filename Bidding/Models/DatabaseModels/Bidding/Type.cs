using System;
using System.Collections.Generic;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Type
    {
        public Type()
        {
            CategoryTypes = new HashSet<CategoryType>();
            TypeProducts = new HashSet<TypeProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        public ICollection<CategoryType> CategoryTypes { get; set; }
        public ICollection<TypeProduct> TypeProducts { get; set; }
    }
}
