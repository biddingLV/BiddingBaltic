namespace Auctions {
  export interface AboutAuctionModel {
    auctionTopCategoryId: number;
    auctionSubCategoryId: number;
    auctionName: string;
    auctionStartingPrice: number;
    auctionValueAddedTax: boolean;
    auctionFormatId: number;
    auctionStartDate?: Date;
    auctionApplyTillDate: Date;
    auctionEndDate: Date;
  }
}
