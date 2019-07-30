// angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

// 3rd lib
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

// internal
import { ExceptionsService } from '../../core/services/exceptions/exceptions.service';
import { UserDetailsModel } from '../models/details/user-details.model';
import { UserListResponseModel } from '../../admin/models/list/user-list-response.model';
import { UserListRequestModel } from '../../admin/models/list/user-list-request.model';


@Injectable({
  providedIn: 'root'
})
export class UsersService {
  constructor(
    private httpClient: HttpClient,
    private exceptionService: ExceptionsService
  ) { }

  /**
 * Loads all(active & inactive) users for admin page
 * @param request
 */
  getUsers$(request: UserListRequestModel): Observable<UserListResponseModel> {
    const url = '/api/users/search';

    const params = new HttpParams({
      fromObject: {
        sortByColumn: request.sortByColumn.toString(),
        sortingDirection: request.sortingDirection.toString(),
        offsetEnd: request.offsetEnd.toString(),
        offsetStart: request.offsetStart.toString(),
        searchValue: request.searchValue === undefined ? '' : request.searchValue.toString(),
        currentPage: request.currentPage.toString()
      }
    });

    return this.httpClient.get<UserListResponseModel>(url, { params })
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getUserDetails$(userId: number): Observable<UserDetailsModel> {
    const url = `/api/users/details?userId=${userId}`;

    return this.httpClient.get<UserDetailsModel>(url)
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  getUserDetailsForEdit$(userId: number): Observable<UserDetailsModel> {
    const url = `/api/users/editDetails?userId=${userId}`;

    return this.httpClient.get<any>(url) // todo: kke: specify type!
      .pipe(catchError(this.exceptionService.errorHandler));
  }

  // editUser$(request: IUserEditRequest): Observable<boolean> {
  //   const url = '/api/users/edit';

  //   return this.http.put<boolean>(url, request)
  //     .pipe(catchError(this.exception.errorHandler));
  // }
}
