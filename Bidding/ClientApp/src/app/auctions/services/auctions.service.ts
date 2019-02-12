// angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

// 3rd lib
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

// internal
import { AuctionModel } from '../models/list/auction.model';
import { AuctionListRequest } from '../models/list/auction-list-request.model';
import { AuctionDetailsModel } from '../models/details/auction-details.model';
import { ExceptionsService } from '../../core/services/exceptions/exceptions.service';
import { AuctionAddRequest } from '../models/add/auction-add-request.model';
import { AuctionEditRequest } from '../models/edit/auction-edit-request.model';
import { AuctionFilterModel } from '../models/filters/auction-filter.model';


@Injectable({
  providedIn: 'root'
})
export class AuctionsService {
  constructor(
    private http: HttpClient,
    private exception: ExceptionsService
  ) { }

  getAuctions$(request: AuctionListRequest): Observable<AuctionModel> {
    const url = '/api/auctions/search'

    const params = new HttpParams({
      fromObject: {
        startDate: request.auctionStartDate.toString(),
        endDate: request.auctionEndDate.toString(),
        sortByColumn: request.sortByColumn.toString(),
        sortingDirection: request.sortingDirection.toString(),
        offsetEnd: request.sizeOfPage.toString(),
        offsetStart: request.currentPage.toString(),
        searchValue: request.searchValue.toString(),
        currentPage: request.currentPage.toString(),
        topCategoryIds: request.topCategoryIds === undefined ? '' : request.topCategoryIds.toString()
      }
    });

    return this.http.get<AuctionModel>(url, { params })
      .pipe(catchError(this.exception.errorHandler));
  }

  getFilters$(): Observable<AuctionFilterModel> {
    const url = '/api/auctions/filters'

    return this.http.get<AuctionFilterModel>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  getAuctionDetails$(auctionId: string): Observable<AuctionDetailsModel> {
    const url = `api/auctions/details?auctionId=${auctionId}`;

    return this.http.get<AuctionDetailsModel>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  addAuction$(request: AuctionAddRequest): Observable<boolean> {
    const url = '/api/auctions/create';

    return this.http.post<boolean>(url, request)
      .pipe(catchError(this.exception.errorHandler));
  }

  editAuction$(request: AuctionEditRequest): Observable<boolean> {
    const url = '/api/auctions/edit';

    return this.http.put<boolean>(url, request)
      .pipe(catchError(this.exception.errorHandler));
  }
}
