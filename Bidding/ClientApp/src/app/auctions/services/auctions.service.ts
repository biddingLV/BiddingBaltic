import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
// import { Observable } from 'rxjs/Observable';

// import { Auth0Service } from '../../../auth/services/auth/auth0.service';
import { IAuctionListResponse } from '../models/auction-list-response.model';
import { IAuctionListRequest } from '../models/auction-list-request.model';
import { Observable } from 'rxjs';


@Injectable()
export class AuctionsService {
  public API_URL = 'http://localhost:3010/api';
  constructor(private http: HttpClient) { }

  public getAuctions(request: IAuctionListRequest): Observable<IAuctionListResponse> {
    const url = '/auctions/search';

    const params = new HttpParams({
      fromObject: {
        SortByColumn: request.SortByColumn.toString(),
        SortingDirection: request.SortingDirection.toString(),
        OffsetEnd: request.OffsetEnd.toString(),
        OffsetStart: request.OffsetStart.toString(),
        SearchValue: request.SearchValue.toString()
      }
    });

    return this.http.get<IAuctionListResponse>(`${this.API_URL + url}`, { headers: this.getAuthHeader(), params });
  }

  public getAuthHeader() {
    return new HttpHeaders()
      .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`);
  }

  // public ping(): void {
  //   this.message = '';
  //   this.http.get<IApiResponse>(`${this.API_URL}/public`)
  //     .subscribe(
  //       data => this.message = data.message,
  //       error => this.message = error
  //     );
  // }
}
