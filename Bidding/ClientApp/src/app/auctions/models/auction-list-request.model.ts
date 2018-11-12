import { IListRequest } from '../../shared/basemodels/list-request.model';

export interface IAuctionListRequest extends IListRequest {
  starDate: Date;
  endDate: Date;
}
