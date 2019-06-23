namespace Auctions {
  export interface AboutAuctionModel {
    auctionCreator: string;
    auctionAddress: string;
    auctionFormat: number;
    auctionCreatorEmail: string;
    auctionCreatorPhone: string;
    auctionStartDate: string;
    auctionApplyTillDate: string;
    auctionEndDate: string;
  }
}