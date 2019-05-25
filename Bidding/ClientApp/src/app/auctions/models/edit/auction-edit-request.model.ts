export interface AuctionEditRequest {
  auctionId: number;
  auctionName: string;
  auctionStartingPrice: number;
  auctionStartDate: Date;
  auctionEndDate: Date;
  auctionStatusName: string;
}
