import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders, HttpErrorResponse } from '@angular/common/http';

import { IAuctionListResponse } from '../models/interfaces/auction-list-response.model';
import { IAuctionListRequest } from '../models/interfaces/auction-list-request.model';
import { AuthService } from '../../auth/auth.service';
import { catchError } from 'rxjs/operators';
import { throwError as ObservableThrowError, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';


@Injectable()
export class AuctionsService {
  private baseUrl = environment.baseUrl;

  constructor(private http: HttpClient, private auth: AuthService) { }

  private get _authHeader(): string {
    return `Bearer ${this.auth.accessToken}`;
  }

  public getAuctions$(request: IAuctionListRequest): Observable<IAuctionListResponse[]> {
    const url = this.baseUrl + '/auctions/search';

    const params = new HttpParams({
      fromObject: {
        SortByColumn: request.SortByColumn.toString(),
        SortingDirection: request.SortingDirection.toString(),
        OffsetEnd: request.OffsetEnd.toString(),
        OffsetStart: request.OffsetStart.toString(),
        SearchValue: request.SearchValue.toString()
      }
    });

    return this.http
      .get<IAuctionListResponse[]>(url, {
        headers: new HttpHeaders().set('Authorization', this._authHeader), params
      })
      .pipe(
        catchError((error) => this._handleError(error))
      );
  }

  private _handleError(err: HttpErrorResponse | any): Observable<any> {
    const errorMsg = err.message || 'Error: Unable to complete request.';
    if (err.message && err.message.indexOf('No JWT present') > -1) {
      this.auth.login();
    }
    return ObservableThrowError(errorMsg);
  }
}
