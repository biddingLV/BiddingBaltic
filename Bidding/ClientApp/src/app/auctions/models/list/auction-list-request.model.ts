import { BaseListRequestModel } from "../../../shared/models/base-list-request.model";

export interface AuctionListRequestModel extends BaseListRequestModel {
  /** optional - category ids */
  topCategoryIds?: number[];

  /** optional - sub-category ids */
  typeIds?: number[];
}
