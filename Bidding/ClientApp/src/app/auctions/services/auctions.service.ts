// angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

// 3rd lib
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

// internal
import { AuctionModel } from '../models/list/auction.model';
import { AuctionListRequest } from '../models/list/auction-list-request.model';
import { CategoryModel } from '../models/filters/category.model';
import { AuctionDetailsModel } from '../models/details/auction-details.model';
import { ExceptionsService } from '../../core/services/exceptions/exceptions.service';
import { AuctionAddRequest } from '../models/add/auction-add-request.model';

@Injectable()
export class AuctionsService {

  constructor(
    private http: HttpClient,
    private exception: ExceptionsService
  ) { }

  getAuctions$(request: AuctionListRequest): Observable<AuctionModel> {
    const url = '/api/auctions/search'

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

    return this.http.get<AuctionModel>(url, { params })
      .pipe(catchError(this.exception.errorHandler));
  }

  getAuctionDetails$(auctionId: string): Observable<AuctionDetailsModel> {
    const url = `api/auctions/details?auctionId=${auctionId}`;

    return this.http.get<AuctionDetailsModel>(url, {
      headers: new HttpHeaders()
        .set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(catchError(this.exception.errorHandler));
  }

  // filters
  getCategories$(): Observable<CategoryModel[]> {
    const url = 'api/auctions/categories';

    return this.http.get<CategoryModel[]>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  addAuction$(request: AuctionAddRequest): Observable<boolean> {
    const url = '/api/auctions/create';

    return this.http.post<boolean>(url, request)
      .pipe(catchError(this.exception.errorHandler));
  }
}
