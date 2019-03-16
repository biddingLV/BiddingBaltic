import { ListRequest } from '../../../shared/models/list-request.model';

export interface AuctionListRequest extends ListRequest {
  auctionStartDate: string;
  auctionEndDate: string;
  topCategoryIds?: number[]; // optional - category ids
  typeIds?: number[]; // optional - sub-category ids
}
