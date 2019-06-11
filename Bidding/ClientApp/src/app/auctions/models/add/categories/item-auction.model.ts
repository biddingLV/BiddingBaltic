namespace Auctions {
  export interface ItemAuctionModel {
    auctionTopCategoryId: number;
    auctionSubCategoryId: number;
    itemName: string;
    itemModel: string;
    itemManufacturingDate: string;
    itemEvaluation: string;
    itemStartingPrice: number;
  }
}