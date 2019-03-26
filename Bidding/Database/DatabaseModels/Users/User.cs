using BiddingAPI.Models.DatabaseModels.Bidding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string UserFirstName { get; set; }

        [MaxLength(50)]
        public string UserLastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserEmail { get; set; }

        [Required]
        public int UserRoleId { get; set; } // todo: kke: this needs to be fKey or constraint based on role table!

        /// <summary>
        /// format: (identity provider)|(unique id in the provider)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserUniqueIdentifier { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedBy { get; set; } // todo: kke: add foreign key to users table

        [DataType(DataType.Date)]
        public DateTime? LastUpdatedAt { get; set; }
        public int? LastUpdatedBy { get; set; } // todo: kke: add foreign key to users table
        public bool Deleted { get; set; }
    }
}
