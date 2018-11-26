import { ListRequest } from '../../../shared/basemodels/list-request.model';

export interface AuctionListRequest extends ListRequest {
  StarDate: Date;
  EndDate: Date;
}
