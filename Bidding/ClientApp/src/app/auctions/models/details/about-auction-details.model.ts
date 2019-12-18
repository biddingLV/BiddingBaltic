import { AuctionDocumentModel } from "./auction-document.model";

export interface AboutAuctionDetailsModel {
  auctionName: string;
  auctionStartingPrice: number;
  auctionValueAddedTax: string;
  auctionFormat: string;
  auctionStartDate: Date;
  auctionApplyTillDate: Date;
  auctionEndDate: Date;
  itemEvaluation: string;
  auctionImageUrls: string[];
  auctionDocumentUrls: AuctionDocumentModel[];
}
