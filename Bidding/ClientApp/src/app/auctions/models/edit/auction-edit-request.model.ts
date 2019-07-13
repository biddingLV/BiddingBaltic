export interface AuctionEditRequest {
  auctionId: number;
  auctionName: string;
  auctionStartingPrice: number;
  auctionStartDate: Date;
  auctionApplyTillDate: Date;
  auctionEndDate: Date;
  auctionStatusId: string;
}
