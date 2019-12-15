namespace Bidding.Models.ViewModels.Auctions.Shared.Categories
{
    public class ItemAuctionModel
    {
        public string ItemModel { get; set; }
        public int? ItemManufacturingYear { get; set; }
        public int? ItemConditionId { get; set; }
        public string ItemEvaluation { get; set; }
        public int ItemStartingPrice { get; set; }
        public string ItemVolume { get; set; }
        public int? ItemCompanyTypeId { get; set; }
    }
}
