import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../auth/auth.service';
import { environment } from '../../../environments/environment';
import { AuctionModel } from '../models/list/auction.model';
import { catchError } from 'rxjs/operators';
import { ExceptionsService } from 'src/app/core';
import { Observable } from 'rxjs';
import { AuctionListRequest } from '../models/list/auction-list-request.model';
import { CategoryModel } from '../models/filters/category.model';
import { AuctionDetailsModel } from '../models/details/auction-details.model';

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

  getAuctions$(request: AuctionListRequest): Observable<AuctionModel> {
    const url = this.baseUrl + '/auctions/search';

    const params = new HttpParams({
      fromObject: {
        startDate: request.starDate.toString(),
        endDate: request.endDate.toString(),
        sortByColumn: request.sortByColumn.toString(),
        sortingDirection: request.sortingDirection.toString(),
        offsetEnd: request.sizeOfPage.toString(),
        offsetStart: request.currentPage.toString(),
        searchValue: request.searchValue.toString(),
        currentPage: request.currentPage.toString()
      }
    });

    return this.http.get<AuctionModel>(url, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`), params
    }).pipe(catchError(this.exception.errorHandler));
  }

  getAuctionDetails$(auctionId: string): Observable<AuctionDetailsModel> {
    const url = this.baseUrl + `/auctions/details?organizationId=${auctionId}`;

    return this.http.get<AuctionDetailsModel>(url, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(catchError(this.exception.errorHandler));
  }

  // filters
  getCategories$(): Observable<CategoryModel[]> {
    const url = this.baseUrl + '/auctions/categories';

    return this.http.get<CategoryModel[]>(url, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(catchError(this.exception.errorHandler));
  }
}
