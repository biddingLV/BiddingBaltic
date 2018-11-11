export interface AuctionModel extends AuctionItem {
  auctions: AuctionItem[];
  currentPage: number;
  itemCount: number;
  offset: number;
  totalPages: number;
}
export interface AuctionItem {
  startDate: Date,
  endDate: Date,
  price: number,
  name: string,
  description: string,
  id: number,
  creator: string,
  creatorId: number
}
