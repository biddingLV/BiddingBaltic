import { AuctionItemModel } from "../shared/auction-item.model";

export interface AuctionModel {
  auctions: AuctionItemModel[];
  currentPage: number;
  itemCount: number;
}
