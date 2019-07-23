import { BaseListRequestModel } from '../../../shared/models/base-list-request.model';

export interface AuctionListRequest extends BaseListRequestModel {
  auctionStartDate: string;
  auctionEndDate: string;
  topCategoryIds?: number[]; // optional - category ids
  typeIds?: number[]; // optional - sub-category ids
}
