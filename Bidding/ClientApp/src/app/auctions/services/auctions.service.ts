import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../auth/auth.service';
import { environment } from '../../../environments/environment';
import { AuctionModel } from '../models/list/auction.model';
import { catchError } from 'rxjs/operators';
import { ExceptionsService } from 'src/app/core';
import { Observable } from 'rxjs';
import { IAuctionListRequest } from '../models/auction-list-request.model';

@Injectable()
export class AuctionsService {
  private baseUrl = environment.baseUrl;

  constructor(
    private http: HttpClient,
    private auth: AuthService,
    private exception: ExceptionsService
  ) { }

  private get authHeader(): string {
    return `Bearer ${this.auth.accessToken}`;
  }

  getAuctions$(request: IAuctionListRequest): Observable<AuctionModel[]> {
    const url = this.baseUrl + '/auctions/search';

    const params = new HttpParams({
      fromObject: {
        SortByColumn: '', //request.SortByColumn.toString(),
        SortingDirection: '',// request.SortingDirection.toString(),
        OffsetEnd: '1', //request.OffsetEnd.toString(),
        OffsetStart: '1', //request.OffsetStart.toString(),
        SearchValue: '', //request.SearchValue.toString()
      }
    });

    return this.http.get<AuctionModel[]>(url, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`), params
    })
      .pipe(catchError(this.exception.errorHandler));
  }
}
