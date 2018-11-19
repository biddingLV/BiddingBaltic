using System;
using System.ComponentModel.DataAnnotations;

namespace Bidding.Models.DatabaseModels.Bidding
{
    public class AuctionCreator
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
    }
}
