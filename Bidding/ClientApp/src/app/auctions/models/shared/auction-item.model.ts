export interface AuctionItemModel {
  auctionId: number;
  auctionName: string;
  auctionStartingPrice: number;
  auctionStartDate: Date;
  auctionEndDate: Date;
  auctionStatusName: string; // todo: kke: this needs to be Status id!
}
