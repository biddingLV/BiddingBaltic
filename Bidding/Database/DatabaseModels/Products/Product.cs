using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Product
    {
        public Product()
        {
            ProductDetails = new HashSet<ProductDetail>();
            TypeProducts = new HashSet<TypeProduct>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required] 
        [MaxLength(50)]
        public string ProductName { get; set; }

        public ICollection<ProductDetail> ProductDetails { get; set; }
        public ICollection<TypeProduct> TypeProducts { get; set; }
    }
}
