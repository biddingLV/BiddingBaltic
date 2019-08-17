// angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

// 3rd lib
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

// internal
import { AuctionListResponseModel } from '../models/list/auction-list-response.model';
import { AuctionListRequestModel } from '../models/list/auction-list-request.model';
import { AuctionDetailsModel } from '../models/details/auction-details.model';
import { ExceptionsService } from '../../core/services/exceptions/exceptions.service';
import { AuctionEditRequestModel } from '../models/edit/auction-edit-request.model';
import { AuctionFilterModel } from '../models/filters/auction-filter.model';
import { AuctionFormatModel } from '../models/add/auction-format.model';
import { AuctionCreatorModel } from '../models/add/auction-creator.model';
import { AuctionStatusModel } from '../models/add/auction-status.model';
import { AuctionDeleteRequest } from '../models/delete/auction-delete-request.model';
import { CategoriesWithTypesModel } from '../models/add/categories-with-types.model';
import { AuctionEditDetailsResponseModel } from '../models/edit/auction-edit-details-response.model';


@Injectable({
  providedIn: 'root'
})
export class AuctionsService {
  constructor(
    private httpService: HttpClient,
    private exceptionService: ExceptionsService
  ) { }

  getAuctions$(request: AuctionListRequestModel): Observable<AuctionListResponseModel> {
    const url = '/api/auctions/list';

    let params = new HttpParams({
      fromObject: {
        sortByColumn: request.sortByColumn.toString(),
        sortingDirection: request.sortingDirection.toString(),
        offsetEnd: request.offsetEnd.toString(),
        offsetStart: request.offsetStart.toString(),
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

    return this.httpService.get<AuctionListResponseModel>(url, { params })
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  /** Loads filters for auction list */
  getFilters$(): Observable<AuctionFilterModel> {
    const url = '/api/auctions/filters';

    return this.httpService.get<AuctionFilterModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  /** Fetch all top-categories with sub-categories / types for auction add wizard */
  categoriesWithTypes$(): Observable<CategoriesWithTypesModel> {
    const url = '/api/auctions/CategoriesWithTypes';

    return this.httpService.get<CategoriesWithTypesModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getAuctionCreators$(): Observable<AuctionCreatorModel[]> {
    const url = '/api/auctions/creators';

    return this.httpService.get<AuctionCreatorModel[]>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getAuctionFormats$(): Observable<AuctionFormatModel[]> {
    const url = '/api/auctions/formats';

    return this.httpService.get<AuctionFormatModel[]>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getAuctionStatuses$(): Observable<AuctionStatusModel[]> {
    const url = '/api/auctions/statuses';

    return this.httpService.get<AuctionStatusModel[]>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getAuctionDetails$(auctionId: number): Observable<AuctionDetailsModel> {
    const url = `api/auctions/details?auctionId=${auctionId}`;

    return this.httpService.get<AuctionDetailsModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  /**
   * Adds a new auction, it can be item, vehicle or property - auction
   * @param request Global auction add request
   */
  addAuction$(request: Auctions.AddAuctionRequestModel): Observable<boolean> {
    const url = '/api/auctions/create';

    return this.httpService.post<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getAuctionEditDetails$(auctionId: number): Observable<AuctionEditDetailsResponseModel> {
    const url = `api/auctions/editDetails?auctionId=${auctionId}`;

    return this.httpService.get<AuctionEditDetailsResponseModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  editAuction$(request: AuctionEditRequestModel): Observable<boolean> {
    const url = '/api/auctions/edit';

    return this.httpService.put<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  deleteAuction$(request: AuctionDeleteRequest): Observable<boolean> {
    const url = '/api/auctions/delete';
    const options = {
      headers: {},
      body: request
    };

    return this.httpService.delete<boolean>(url, options)
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
