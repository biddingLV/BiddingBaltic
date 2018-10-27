import { IListResponse } from '../../shared/basemodels/list-response.model';

export interface IAuctionListResponse extends IListResponse {
  Auctions: IAuctionItem[];
}
export interface IAuctionItem {
  Id: number;
  Description: string;
  Brand: string;
  Price: number;
  Type: string;
}
