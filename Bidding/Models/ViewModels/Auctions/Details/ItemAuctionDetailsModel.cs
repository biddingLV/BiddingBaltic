using System.ComponentModel.DataAnnotations.Schema;

namespace Bidding.Models.ViewModels.Auctions.Details
{
    public class ItemAuctionDetailsModel
    {
        public string ItemModel { get; set; }
        public int? ItemManufacturingYear { get; set; }
        public string ItemConditionName { get; set; }
        public string ItemVolume { get; set; }
        public string ItemCompanyType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ItemStartingPrice { get; set; }
    }
}
