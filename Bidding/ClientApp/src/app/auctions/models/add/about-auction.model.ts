namespace Auctions {
  export interface AboutAuctionModel {
    auctionTopCategoryId: number;
    auctionSubCategoryId?: number;
    auctionName: string;
    auctionStartingPrice: number;
    auctionFormatId: number;
    auctionStartDate?: Date;
    auctionApplyTillDate: Date;
    auctionEndDate: Date;
  }
}