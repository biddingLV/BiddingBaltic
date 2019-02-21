export interface AuctionModel extends AuctionItem {
  auctions: AuctionItem[];
  currentPage: number;
  itemCount: number;
}
export interface AuctionItem {
  auctionId: number;
  auctionName: string;
  auctionStartingPrice: number;
  auctionStartDate: Date;
  auctionEndDate: Date;
  auctionStatusName: string;
}
