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

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastUpdatedAt { get; set; }
        public int LastUpdatedBy { get; set; }
        public bool? Deleted { get; set; }

        public ICollection<CategoryType> CategoryTypes { get; set; }
    }
}
