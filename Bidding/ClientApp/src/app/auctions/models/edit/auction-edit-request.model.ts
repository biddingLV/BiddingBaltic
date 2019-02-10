export interface AuctionEditRequest {
  auctionId: number;
  auctionName: string;
  description: string;
  startingPrice: string;
  // StartDate: string;
  creator: string;
}
