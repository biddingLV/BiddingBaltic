namespace Auctions {
  export interface AboutAuctionModel {
    auctionCreator: string;
    auctionAddress: string;
    auctionCreatorEmail: string;
    auctionCreatorPhone: string;
    auctionFormat: number;
    auctionStartDate: Date;
    auctionApplyTillDate: Date;
    auctionEndDate: Date;
  }
}
