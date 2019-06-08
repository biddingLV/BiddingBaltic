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
import { AuctionEditRequest } from '../models/edit/auction-edit-request.model';
import { AuctionFilterModel } from '../models/filters/auction-filter.model';
import { AuctionFormatModel } from '../models/add/auction-format.model';
import { AuctionCreatorModel } from '../models/add/auction-creator.model';
import { AuctionStatusModel } from '../models/add/auction-status.model';
import { AuctionDeleteRequest } from '../models/delete/auction-delete-request.model';


@Injectable({
  providedIn: 'root'
})
export class AuctionsService {
  constructor(
    private http: HttpClient,
    private exception: ExceptionsService
  ) { }

  getAuctions$(request: AuctionListRequest): Observable<AuctionModel> {
    const url = '/api/auctions/search';

    let params = new HttpParams({
      fromObject: {
        startDate: request.auctionStartDate.toString(),
        endDate: request.auctionEndDate.toString(),
        sortByColumn: request.sortByColumn.toString(),
        sortingDirection: request.sortingDirection.toString(),
        offsetEnd: request.sizeOfPage.toString(),
        offsetStart: request.currentPage.toString(),
        searchValue: request.searchValue === undefined ? '' : request.searchValue.toString(),
        currentPage: request.currentPage.toString()
      }
    });

    if (request.topCategoryIds !== undefined) {
      for (const id of request.topCategoryIds) {
        params = params.append('topCategoryIds', id.toString());
      }
    }

    if (request.typeIds !== undefined) {
      for (const id of request.typeIds) {
        params = params.append('typeIds', id.toString());
      }
    }

    return this.http.get<AuctionModel>(url, { params })
      .pipe(catchError(this.exception.errorHandler));
  }

  getFilters$(): Observable<AuctionFilterModel> {
    const url = '/api/auctions/filters';

    return this.http.get<AuctionFilterModel>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  getAuctionCreators$(): Observable<AuctionCreatorModel[]> {
    const url = '/api/auctions/creators';

    return this.http.get<AuctionCreatorModel[]>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  getAuctionFormats$(): Observable<AuctionFormatModel[]> {
    const url = '/api/auctions/formats';

    return this.http.get<AuctionFormatModel[]>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  getAuctionStatuses$(): Observable<AuctionStatusModel[]> {
    const url = '/api/auctions/statuses';

    return this.http.get<AuctionStatusModel[]>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  getAuctionDetails$(auctionId: number): Observable<AuctionDetailsModel> {
    const url = `api/auctions/details?auctionId=${auctionId}`;

    return this.http.get<AuctionDetailsModel>(url)
      .pipe(catchError(this.exception.errorHandler));
  }

  /**
   * Adds a new auction, it can be item, vehicle or property - auction
   * @param request Global auction add request
   */
  addAuction$(request: Auctions.AddAuctionRequestModel): Observable<boolean> {
    const url = '/api/auctions/create';

    return this.http.post<boolean>(url, request)
      .pipe(catchError(this.exception.errorHandler));
  }

  editAuction$(request: AuctionEditRequest): Observable<boolean> {
    const url = '/api/auctions/edit';

    return this.http.put<boolean>(url, request)
      .pipe(catchError(this.exception.errorHandler));
  }

  deleteAuction$(request: AuctionDeleteRequest): Observable<boolean> {
    const url = '/api/auctions/delete';
    const options = {
      headers: {},
      body: request
    };

    return this.http.delete<boolean>(url, options)
      .pipe(catchError(this.exception.errorHandler));
  }
}
