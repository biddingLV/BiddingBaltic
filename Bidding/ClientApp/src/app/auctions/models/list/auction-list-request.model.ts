import { ListRequest } from '../../../shared/basemodels/list-request.model';

export interface AuctionListRequest extends ListRequest {
  starDate: Date;
  endDate: Date;
}
