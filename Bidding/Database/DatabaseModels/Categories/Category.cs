using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Category
    {
        public Category()
        {
            CategoryTypes = new HashSet<CategoryType>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }

        [Required]
        public bool CategoryStatus { get; set; }

        public ICollection<CategoryType> CategoryTypes { get; set; }
    }
}
