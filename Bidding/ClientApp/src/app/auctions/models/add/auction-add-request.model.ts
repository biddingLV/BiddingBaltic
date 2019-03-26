export interface AuctionAddRequest {
  auctionName: string;
  auctionTopCategoryId: string;
  auctionSubCategoryId: string;
  auctionFormatId: string,
  auctionDescription: string;
  auctionStartingPrice: string;
  auctionStartDate: string;
  auctionEndDate: string;
  auctionTillDate: string;
  auctionCreator: string;
}
