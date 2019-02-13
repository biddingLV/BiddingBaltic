using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class CategoryType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryTypeId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int TypeId { get; set; }

        public Category Category { get; set; }
        public Type Type { get; set; }
    }
}
