using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BiddingAPI.Models.DatabaseModels
{
    public partial class User
    {
        public User()
        {
            //UserDetails = new HashSet<UserDetails>();
            //UserOrganizations = new HashSet<UserOrganization>();
        }

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
        public bool UserStatus { get; set; }

        [Required]
        public int UserRoleId { get; set; } // todo: kke: this needs to be fKey or constraint based on role table!

        /// <summary>
        /// format: (identity provider)|(unique id in the provider)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string UserUniqueIdentifier { get; set; }

        //public ICollection<UserDetails> UserDetails { get; set; }
        //public ICollection<UserOrganization> UserOrganizations { get; set; }
    }
}
