using Bidding.Database.DatabaseModels.Auctions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BiddingAPI.Models.DatabaseModels.Bidding
{
    // note: kke: this probably gonna be MasterDetails model!
    // and then sub-models based on specific categories
    public class AuctionDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuctionDetailsId { get; set; }

        // todo: kke: this is not for all auctions!
        //public string VehicleRegistrationNumber { get; set; }
        //public string VehicleIdentificationNumber { get; set; }
        //public string Year { get; set; } // only year needed
        //public int Evaluation { get; set; }
        //public string AuctionType { get; set; }

        [Required]
        public int AuctionStatusId { get; set; }

        [Required]
        public int AuctionId { get; set; }

        [Required]
        public int AuctionFormatId { get; set; }

        public int AuctionConditionId { get; set; }

        public string AuctionDescription { get; set;}

        public AuctionFormat AuctionFormat { get; set; }
        public AuctionCondition AuctionCondition { get; set; }
        public Auction Auction { get; set; }
        public AuctionStatus AuctionStatus { get; set; }

    }
}
