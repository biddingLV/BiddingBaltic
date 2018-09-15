import { IListResponse } from '../../shared/basemodels/list-response.model';

export interface IAuctionListResponse extends IListResponse {
  Auctions: IAuctionList[];
}
export interface IAuctionList {
  Id: number;
  Description: string;
  Brand: string;
  Price: number;
  Type: string;
}
