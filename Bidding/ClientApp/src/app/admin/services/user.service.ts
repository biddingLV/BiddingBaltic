// angular
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

// 3rd lib
import { catchError } from 'rxjs/operators';
import { Observable } from 'rxjs';

// internal
import { ExceptionsService } from '../../core';
import { UserListResponseModel } from '../models/list/user-list-response.model';
import { UserListRequestModel } from '../models/list/user-list-request.model';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private httpService: HttpClient,
    private exceptionService: ExceptionsService
  ) { }

  // todo: kke: SPECIFY ALL MISSING TYPES!
  getUsers$(request: UserListRequestModel): Observable<UserListResponseModel> {
    const url = '/api/users/search';

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

    return this.httpService.get<UserListResponseModel>(url, { params })
      .pipe(catchError(this.exceptionService.errorHandler));
  }
}
