// angular
import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";

// 3rd lib
import { Observable } from "rxjs";
import { catchError } from "rxjs/operators";

// internal
import { ExceptionsService } from "../../core/services/exceptions/exceptions.service";
import { UserDetailsResponseModel } from "../models/details/user-details-response.model";
import { UserListResponseModel } from "../../admin/models/list/user-list-response.model";
import { UserListRequestModel } from "../../admin/models/list/user-list-request.model";
import { UserEditRequestModel } from "../models/edit/user-edit-request.model";

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

  getUserDetails$(userId: number): Observable<UserDetailsResponseModel> {
    const url = `/api/users/details?userId=${userId}`;

    return this.httpClient
      .get<UserDetailsResponseModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getUserDetailsForEdit$(userId: number): Observable<UserDetailsResponseModel> {
    const url = `/api/users/editDetails?userId=${userId}`;

    return this.httpClient
      .get<UserDetailsResponseModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  editUser$(request: UserEditRequestModel): Observable<boolean> {
    const url = "/api/users/edit";

    return this.httpClient
      .put<boolean>(url, request)
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
