import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../auth/auth.service';
import { environment } from '../../../environments/environment';
import { AuctionModel } from '../models/list/auction.model';
import { catchError, delay } from 'rxjs/operators';
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
        startDate: '10/29/2018'.toString(),
        endDate: '10/31/2018'.toString(),
        SortByColumn: '', //request.SortByColumn.toString(),
        SortingDirection: '',// request.SortingDirection.toString(),
        OffsetEnd: '', //request.OffsetEnd.toString(),
        OffsetStart: '', //request.OffsetStart.toString(),
        SearchValue: '', //request.SearchValue.toString()
      }
    });

    return this.http.get<AuctionModel[]>(url, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`), params
    })
      .pipe(delay(2000))
    // .pipe(catchError(this.exception.errorHandler));
  }
}
