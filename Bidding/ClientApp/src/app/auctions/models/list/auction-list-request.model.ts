import { ListRequest } from '../../../shared/basemodels/list-request.model';

export interface AuctionListRequest extends ListRequest {
  auctionStartDate: string;
  auctionEndDate: string;
}
