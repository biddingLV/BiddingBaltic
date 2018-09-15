import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';


@Injectable()
export class BlogService {
  public API_URL = 'http://localhost:3010/api';
  constructor(private http: HttpClient) { }

  // public getAuctions(request: IAuctionListRequest): Observable<IAuctionListResponse> {
  //   const url = '/auctions/search';

  //   const params = new HttpParams({
  //     fromObject: {
  //       // SortColumn: request.SortColumn.toString(),
  //       // SortDirection: request.SortDirection.toString(),
  //       // Search: request.Search.toString(),
  //       // IncludeChildren: request.IncludeChildren ? 'true' : 'false',
  //       // OrgId: request.OrgId.toString(),
  //       PageSize: request.PageSize.toString(),
  //       Page: request.Page.toString()
  //     }
  //   });

  //   return this.http.get<IAuctionListResponse>(`${this.API_URL + url}`, { headers: this.getAuthHeader(), params });
  // }

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
