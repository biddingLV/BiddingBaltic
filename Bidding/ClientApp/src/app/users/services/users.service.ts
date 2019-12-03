// angular
import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

// 3rd lib
import { Observable } from "rxjs";
import { catchError } from "rxjs/operators";

// internal
import { ExceptionsService } from "../../core/services/exceptions/exceptions.service";

import { UserListResponseModel } from "../../admin/models/list/user-list-response.model";
import { UserListRequestModel } from "../../admin/models/list/user-list-request.model";
import { EditBasicDetailsRequestModel } from "../models/edit/edit-basic-details-request.model";
import { UserAdvancedDetailsResponseModel } from "../models/details/user-advanced-details-response.model";
import { UserBasicDetailsResponseModel } from "../models/details/user-basic-details-response.model";
import { EditAdvancedDetailsRequestModel } from "../models/edit/edit-advanced-details-request.model";

@Injectable({
  providedIn: "root"
})
export class UsersService {
  constructor(
    private httpClient: HttpClient,
    private exceptionService: ExceptionsService
  ) {}

  /**
   * Loads all(active & inactive) users for admin page
   * @param request
   */
  getUsers$(request: UserListRequestModel): Observable<UserListResponseModel> {
    const url = "/api/users/search";

    const params = new HttpParams({
      fromObject: {
        sortByColumn: request.sortByColumn.toString(),
        sortingDirection: request.sortingDirection.toString(),
        offsetEnd: request.offsetEnd.toString(),
        offsetStart: request.offsetStart.toString(),
        searchValue:
          request.searchValue === undefined
            ? ""
            : request.searchValue.toString(),
        currentPage: request.currentPage.toString()
      }
    });

    return this.httpClient
      .get<UserListResponseModel>(url, { params })
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getUserDetails$(userId: number): Observable<UserBasicDetailsResponseModel> {
    const url = `/api/users/details?userId=${userId}`;

    return this.httpClient
      .get<UserBasicDetailsResponseModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getDetailsForBasicEdit$(
    userId: number
  ): Observable<UserBasicDetailsResponseModel> {
    const url = `/api/users/EditBasicDetails?userId=${userId}`;

    return this.httpClient
      .get<UserBasicDetailsResponseModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getDetailsForAdvancedEdit$(
    userId: number
  ): Observable<UserAdvancedDetailsResponseModel> {
    const url = `/api/users/EditAdvancedDetails?userId=${userId}`;

    return this.httpClient
      .get<UserAdvancedDetailsResponseModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  editBasicDetails$(
    request: EditBasicDetailsRequestModel
  ): Observable<boolean> {
    const url = "/api/users/EditBasic";

    return this.httpClient
      .put<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  editAdvancedDetails$(
    request: EditAdvancedDetailsRequestModel
  ): Observable<boolean> {
    const url = "/api/users/EditAdvanced";

    return this.httpClient
      .put<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
