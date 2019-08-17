import { BaseListResponseModel } from "ClientApp/src/app/shared/models/base-list-response.model";
import { AuctionListItemModel } from "./auction-list-item.model";

export interface AuctionListResponseModel extends BaseListResponseModel {
  auctions: AuctionListItemModel[];
}
