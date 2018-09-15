import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

@Injectable()
export class AdminService {
  public API_URL = 'http://localhost:3010/api';
  constructor(private http: HttpClient) { }

  // public getAuctions(request: IAuctionListRequest): Observable<IAuctionListResponse> {
  //   const url = '/auctions/search';

  //   const params = new HttpParams({
  //     fromObject: {
  //       SortByColumn: request.SortByColumn.toString(),
  //       SortingDirection: request.SortingDirection.toString(),
  //       OffsetEnd: request.OffsetEnd.toString(),
  //       OffsetStart: request.OffsetStart.toString(),
  //       SearchValue: request.SearchValue.toString()
  //     }
  //   });

  //   return this.http.get<IAuctionListResponse>(`${this.API_URL + url}`, { headers: this.getAuthHeader(), params });
  // }

  // public getAuthHeader() {
  //   return new HttpHeaders()
  //     .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`);
  // }

  // public ping(): void {
  //   this.message = '';
  //   this.http.get<IApiResponse>(`${this.API_URL}/public`)
  //     .subscribe(
  //       data => this.message = data.message,
  //       error => this.message = error
  //     );
  // }
}
