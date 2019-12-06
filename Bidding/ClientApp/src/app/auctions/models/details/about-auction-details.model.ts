namespace Auctions {
  export interface AboutAuctionDetailsModel {
    auctionName: string;
    auctionStartingPrice: number;
    auctionFormat: string;
    auctionStartDate: Date;
    auctionApplyTillDate: Date;
    auctionEndDate: Date;
    auctionFiles: string[];
  }
}
