export interface AuctionAddRequest {
  auctionName: string;
  auctionTopCategoryIds: number[];
  auctionSubCategoryIds: number[];
  auctionStartingPrice: number;
  auctionStartDate: string;
  auctionApplyTillDate: string;
  auctionEndDate: string;
  auctionDescription: string;
  auctionCreatorId: number;
  auctionFormatId: number;
  auctionStatusId: number;
}
