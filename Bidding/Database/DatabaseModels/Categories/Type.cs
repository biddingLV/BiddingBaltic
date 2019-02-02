using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class Type
    {
        public Type()
        {
            CategoryTypes = new HashSet<CategoryType>();
            TypeProducts = new HashSet<TypeProduct>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TypeName { get; set; }

        [Required]
        public bool TypeStatus { get; set; }

        public ICollection<CategoryType> CategoryTypes { get; set; }
        public ICollection<TypeProduct> TypeProducts { get; set; }
    }
}
